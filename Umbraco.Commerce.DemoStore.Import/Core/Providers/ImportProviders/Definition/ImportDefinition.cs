namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.Definition
{
    public class ImportDefinition
    {
        public Guid Key { get; set; }

        public string DefinitionAlias { get; set; }

        public string DefinitionName { get; set; }

        public string DefinitionIcon { get; set; }

        public bool CanVaryByCulture { get; set; }

        public IEnumerable<CultureDefinition> Cultures { get; set; }

        public string DefinitionType { get; set; }

        public List<ImportMapping> ImportMapping { get; set; }

        public ImportDefinition()
        {
            ImportMapping = new List<ImportMapping>();
            Cultures = new List<CultureDefinition>();
        }

        public IEnumerable<ImportPropertyInfo> GetMappedPropertyInfo(string culture = null)
        {
            return ImportMapping.FirstOrDefault((ImportMapping mapping) => mapping.Culture == culture)?.PropertyInfo?.Where((ImportPropertyInfo p) => !string.IsNullOrWhiteSpace(p.MappedDataSourceColumn)).ToList();
        }
    }
}
