using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.Definition;
namespace Umbraco.Commerce.DemoStore.Import.Common.Services.Definition
{
    public interface IContentImportDefinitionService
    {
        ImportDefinition GetDefinition(string definitionAlias, bool isDocumeentDefinition = true);

        ImportDefinition GetDefinition(Guid definitionKey, bool isDocumeentDefinition = true);
    }
}
