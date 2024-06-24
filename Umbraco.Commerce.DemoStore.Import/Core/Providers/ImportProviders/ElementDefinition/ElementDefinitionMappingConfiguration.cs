namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition
{
    public class ElementDefinitionMappingConfiguration
    {
        public int? MaxElements { get; set; }

        public IEnumerable<ElementDefinitionConfig> ElementDefinitionConfig { get; set; } = new List<ElementDefinitionConfig>();

    }
}
