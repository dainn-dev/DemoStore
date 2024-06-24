using Umbraco.Commerce.DemoStore.Import.Core.Import;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    public class ImportOptions
    {
        public IMediaImport MediaImport { get; set; }

        public IProviderOptions DataProviderOptions { get; set; }

        public int UserId { get; set; }
    }
}
