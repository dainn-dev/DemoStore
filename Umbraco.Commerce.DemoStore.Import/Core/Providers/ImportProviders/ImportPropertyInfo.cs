using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders.ElementDefinition;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    public class ImportPropertyInfo
    {
        public string PropertyAlias { get; set; }

        public string PropertyName { get; set; }

        public string PropertyEditorAlias { get; set; }

        public Guid DataTypeKey { get; set; }

        public string PropertyTypeGroup { get; set; }

        public bool CanVaryByCulture { get; set; }

        public string MappedDataSourceColumn { get; set; }

        public bool Enabled { get; set; }

        public int GroupOrder { get; set; }

        public int SortOrder { get; set; }

        public ElementDefinitionMapping ElementDefinitionMapping { get; set; }

        public IProviderOptions ProviderOptions { get; set; }

        public IEnumerable<string> ValidationErrors { get; set; }
    }

}
