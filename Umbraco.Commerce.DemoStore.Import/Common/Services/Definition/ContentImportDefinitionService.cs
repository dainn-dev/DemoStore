using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Commerce.DemoStore.Import.Common.Config;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.AdvancedSettings;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.Definition;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services.Definition
{
    public class ContentImportDefinitionService : IContentImportDefinitionService
    {
        private readonly IContentTypeService _contentTypeService;

        private readonly ILocalizationService _localizationService;

        private readonly IAdvancedSettingsRepository _advancedSettingsRepository;

        private readonly ILocalizedTextService _localizedTextService;

        private readonly CmsImportConfig _config;

        private readonly GlobalSettings _globalSettings;

        public ContentImportDefinitionService(IContentTypeService contentTypeService, 
            ILocalizationService localizationService, 
            IAdvancedSettingsRepository advancedSettingsRepository, 
            ILocalizedTextService localizedTextService, 
            IOptions<GlobalSettings> globalSettings, 
            IOptions<CmsImportConfig> config)
        {
            _contentTypeService = contentTypeService;
            _localizationService = localizationService;
            _advancedSettingsRepository = advancedSettingsRepository;
            _localizedTextService = localizedTextService;
            _config = config.Value;
            _globalSettings = globalSettings.Value;
        }

        public ImportDefinition GetDefinition(string definitionAlias, bool isDocumentDefinition = true)
        {
            return GetDefinition(_contentTypeService.Get(definitionAlias), isDocumentDefinition);
        }

        public ImportDefinition GetDefinition(Guid definitionKey, bool isDocumentDefinition = true)
        {
            return GetDefinition(_contentTypeService.Get(definitionKey), isDocumentDefinition);
        }

        public ImportDefinition GetDefinition(IContentType contentDefinition, bool isDocumentDefinition = true)
        {
            ImportDefinition definition = new ImportDefinition
            {
                DefinitionAlias = contentDefinition.Alias,
                DefinitionName = contentDefinition.Name,
                Key = contentDefinition.Key,
                DefinitionIcon = contentDefinition.Icon,
                DefinitionType = "content",
                CanVaryByCulture = ContentVariationExtensions.VariesByCulture(contentDefinition)
            };
            definition.Cultures = from d in _localizationService.GetAllLanguages()
                                  where definition.CanVaryByCulture || d.IsDefault
                                  orderby d.IsDefault descending
                                  select d into l
                                  select new CultureDefinition
                                  {
                                      IsDefault = l.IsDefault,
                                      IsoCode = l.IsoCode,
                                      Name = l.CultureName
                                  };
            List<ImportPropertyInfo> list = new List<ImportPropertyInfo>();
            List<ImportPropertyInfo> list2 = new List<ImportPropertyInfo>();
            if (isDocumentDefinition)
            {
                AddDefaultProperties(list);
            }
            foreach (PropertyGroup compositionPropertyGroup in contentDefinition.CompositionPropertyGroups)
            {
                foreach (IPropertyType item in compositionPropertyGroup.PropertyTypes.Where((IPropertyType p) => !_config.IgnoredPropertyAliasses.Contains(p.Alias) && (!p.IsElementProperty() || isDocumentDefinition)))
                {
                    AdvancedSettingProvider byPropertyEditorAlias = _advancedSettingsRepository.GetByPropertyEditorAlias(item.PropertyEditorAlias);
                    ImportPropertyInfo importPropertyInfo = new ImportPropertyInfo
                    {
                        PropertyEditorAlias = item.PropertyEditorAlias,
                        DataTypeKey = item.DataTypeKey,
                        PropertyAlias = item.Alias,
                        PropertyName = item.Name,
                        PropertyTypeGroup = compositionPropertyGroup.Name,
                        GroupOrder = compositionPropertyGroup.SortOrder,
                        SortOrder = item.SortOrder,
                        CanVaryByCulture = ContentVariationExtensions.VariesByCulture(item),
                        ElementDefinitionMapping = (item.IsElementProperty() ? new ElementDefinitionMapping
                        {
                            ContentTypeAlias = contentDefinition.Alias,
                            ContentTypeName = contentDefinition.Name,
                            PropertyAlias = item.Alias,
                            PropertyName = item.Name,
                            DataTypeKey = item.DataTypeKey,
                            PropertyEditorAlias = item.PropertyEditorAlias,
                            View = ViewHelper.EnsureViewPluginsPath("/App_Plugins/cmsimport/fields/elementeditorfield.html", _globalSettings.UmbracoPath)
                        } : null),
                        Enabled = (byPropertyEditorAlias?.CollapseByDefault ?? false)
                    };
                    importPropertyInfo.ProviderOptions = byPropertyEditorAlias?.GetAdvancedSettingProviderOptions(importPropertyInfo);
                    list2.Add(importPropertyInfo);
                }
            }
            list.AddRange(from p in list2
                          orderby p.GroupOrder, p.PropertyTypeGroup, p.SortOrder
                          select p);
            foreach (CultureDefinition culture in definition.Cultures)
            {
                definition.ImportMapping.Add(new ImportMapping
                {
                    PropertyInfo = list.Where((ImportPropertyInfo c) => culture.IsDefault || c.CanVaryByCulture),
                    Culture = culture.IsoCode,
                    IsDefaultCulture = culture.IsDefault
                });
            }
            return definition;
        }

        private void AddDefaultProperties(List<ImportPropertyInfo> properties)
        {
            AdvancedSettingProvider byPropertyEditorAlias = _advancedSettingsRepository.GetByPropertyEditorAlias("Umbraco.DateTime");
            ImportPropertyInfo item = new ImportPropertyInfo
            {
                PropertyEditorAlias = "Umbraco.TextBox",
                PropertyName = _localizedTextService.Localize("contentImportProvider/nodenameProperty"),
                PropertyAlias = "@@#Import_name",
                PropertyTypeGroup = "Properties",
                CanVaryByCulture = true
            };
            ImportPropertyInfo importPropertyInfo = new ImportPropertyInfo
            {
                PropertyEditorAlias = "Umbraco.DateTime",
                PropertyName = _localizedTextService.Localize("contentImportProvider/creadeDateProperty"),
                PropertyAlias = "@@#Import_createDate",
                PropertyTypeGroup = "Properties",
                Enabled = (byPropertyEditorAlias?.CollapseByDefault ?? false)
            };
            importPropertyInfo.ProviderOptions = byPropertyEditorAlias?.GetAdvancedSettingProviderOptions(importPropertyInfo);
            ImportPropertyInfo importPropertyInfo2 = new ImportPropertyInfo
            {
                PropertyEditorAlias = "Umbraco.DateTime",
                PropertyName = _localizedTextService.Localize("contentImportProvider/publishAtProperty"),
                PropertyAlias = "@@#Import_publishAt",
                PropertyTypeGroup = "Properties",
                Enabled = (byPropertyEditorAlias?.CollapseByDefault ?? false)
            };
            importPropertyInfo2.ProviderOptions = byPropertyEditorAlias?.GetAdvancedSettingProviderOptions(importPropertyInfo2);
            ImportPropertyInfo importPropertyInfo3 = new ImportPropertyInfo
            {
                PropertyEditorAlias = "Umbraco.DateTime",
                PropertyName = _localizedTextService.Localize("contentImportProvider/unpublishAtProperty"),
                PropertyAlias = "@@#Import_unpublishAt",
                PropertyTypeGroup = "Properties",
                Enabled = (byPropertyEditorAlias?.CollapseByDefault ?? false)
            };
            importPropertyInfo3.ProviderOptions = byPropertyEditorAlias?.GetAdvancedSettingProviderOptions(importPropertyInfo3);
            List<ImportPropertyInfo> collection = new List<ImportPropertyInfo> { item, importPropertyInfo, importPropertyInfo2, importPropertyInfo3 };
            properties.AddRange(collection);
        }
    }

}
