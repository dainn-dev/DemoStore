using Umbraco.Cms.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Notifications
{
    public class ImportingNotification : INotification
    {
        public string ProviderAlias { get; set; }

        public ImportState State { get; private set; }

        public ImportingNotification(ImportState state)
        {
            State = state;
            ProviderAlias = state?.ImportProvider?.Alias;
        }
    }
}
