namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition
{
    public class ElementDefinition
    {
        public string ElementTypeAlias { get; set; }

        public string ElementTypeName { get; set; }

        public Guid ElementTypeKey { get; set; }

        public string ElementTypeIcon { get; set; }

        public string ElementPropertyType { get; set; }

        public List<ImportMapping> ImportMapping { get; set; }
    }
}
