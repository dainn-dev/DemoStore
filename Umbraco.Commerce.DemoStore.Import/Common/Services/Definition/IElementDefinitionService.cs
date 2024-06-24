using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services.Definition
{
    public interface IElementDefinitionService
    {
        ElementDefinition GetElementDefinition(Guid elementTypeKey);
    }
}
