using Umbraco.Cms.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Notifications
{
    public class BulkImportingNotification : INotification
    {
        public string ProviderAlias { get; set; }

        public ImportState State { get; private set; }

        public BulkImportingNotification(ImportState state)
        {
            State = state;
            ProviderAlias = state?.ImportProvider?.Alias;
        }
    }
}
