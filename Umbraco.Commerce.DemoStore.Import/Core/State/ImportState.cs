using System.Reflection.Metadata;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.State
{
    public class ImportState
    {
        public DataProvider DataProvider { get; set; }

        public IProviderOptions SelectedDataProviderOptions { get; set; }

        public IProviderOptions SelectedImportProviderOptions { get; set; }

        public ImportProvider ImportProvider { get; set; }

        public ImportDefinition ImportDefinitionOptions { get; set; }

        public string StateName { get; set; }

        public int ImportAsUserId { get; set; }

        public Guid ParentStateId { get; set; }

        public Guid StateId { get; set; }
    }
}
