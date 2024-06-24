using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Sync;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Cache
{
    public class CMSImportCacheRefresherNotification : CacheRefresherNotification
    {
        public CMSImportCacheRefresherNotification(object messageObject, MessageType messageType)
            : base(messageObject, messageType)
        {
        }
    }
}
