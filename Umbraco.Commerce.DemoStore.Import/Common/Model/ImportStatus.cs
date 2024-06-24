using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model
{
    public class ImportStatus
    {
        public ImportResult Result;

        public Guid ImportId { get; set; }

        public string StatusMessage { get; set; }

        public bool Finished { get; set; }

        public bool Canceled { get; set; }
    }
}
