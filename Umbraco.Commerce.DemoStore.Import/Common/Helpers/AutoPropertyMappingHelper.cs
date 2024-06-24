using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class AutoPropertyMappingHelper
    {
        public void AutoMapProperties(IEnumerable<string> datasourceColumns, IEnumerable<ImportMapping> mapping)
        {
            foreach (ImportPropertyInfo prop in (mapping.FirstOrDefault((ImportMapping importMapping) => importMapping.IsDefaultCulture) ?? mapping.First()).PropertyInfo)
            {
                string text = datasourceColumns.FirstOrDefault((string s) => s.Equals(prop.PropertyAlias, StringComparison.InvariantCultureIgnoreCase) || prop.PropertyName.Equals(s, StringComparison.InvariantCultureIgnoreCase));
                prop.MappedDataSourceColumn = ((!string.IsNullOrWhiteSpace(text)) ? text : string.Empty);
            }
        }
    }
}
