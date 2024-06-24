using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.Definition;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services.Definition
{
    public class ElementDefinitionService : IElementDefinitionService
    {
        private readonly IContentImportDefinitionService _contentImportDefinitionService;

        public ElementDefinitionService(IContentImportDefinitionService contentImportDefinitionService)
        {
            _contentImportDefinitionService = contentImportDefinitionService;
        }

        public ElementDefinition GetElementDefinition(Guid elementTypeKey)
        {
            ImportDefinition definition = _contentImportDefinitionService.GetDefinition(elementTypeKey, isDocumeentDefinition: false);
            return new ElementDefinition
            {
                ElementTypeAlias = definition.DefinitionAlias,
                ElementTypeIcon = definition.DefinitionIcon,
                ElementTypeKey = definition.Key,
                ElementTypeName = definition.DefinitionName,
                ImportMapping = definition.ImportMapping
            };
        }
    }
}
