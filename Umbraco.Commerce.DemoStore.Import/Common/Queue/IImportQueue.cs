using Umbraco.Commerce.DemoStore.Import.Common.Model;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Queue
{
    public interface IImportQueue
    {
        Dictionary<ImportStatus, ImportState> Queue { get; }

        void AddToQueue(ImportStatus status, ImportState state);

        ImportResult Start(ImportStatus status, bool scheduled = false);

        void ImportCurrentAndChildren(ImportState state, ImportStatus status, ImportResult result, bool scheduled);

        ImportStatus GetProgress(ImportStatus status);
    }
}
