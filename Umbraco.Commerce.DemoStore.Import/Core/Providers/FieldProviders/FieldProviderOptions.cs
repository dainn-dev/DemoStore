using Umbraco.Commerce.DemoStore.Import.Core.State;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders
{
    public class FieldProviderOptions : ImportOptions
    {
        public ImportState ImportState { get; set; }

        public bool Break { get; set; }
    }
}
