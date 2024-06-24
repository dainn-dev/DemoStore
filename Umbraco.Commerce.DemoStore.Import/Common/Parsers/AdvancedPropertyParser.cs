using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.AdvancedSettings;
using Umbraco.Commerce.DemoStore.Import.Core.Parsers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Parsers
{
    public class AdvancedPropertyParser : IAdvancedPropertyParser
    {
        private readonly IAdvancedSettingsRepository _advancedSettingsRepository;

        public AdvancedPropertyParser(IAdvancedSettingsRepository advancedSettingsRepository)
        {
            _advancedSettingsRepository = advancedSettingsRepository;
        }

        public object Parse(object value, IContentBase importedItem, ImportPropertyInfo property, ImportOptions options)
        {
            AdvancedSettingProvider byPropertyEditorAlias = _advancedSettingsRepository.GetByPropertyEditorAlias(property.PropertyEditorAlias);
            if (byPropertyEditorAlias == null)
            {
                return value;
            }
            return byPropertyEditorAlias.Parse(value, importedItem, property.ProviderOptions, options);
        }
    }
}
