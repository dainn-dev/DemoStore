using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Cache
{
    public class CacheRefresher : CacheRefresherBase<CMSImportCacheRefresherNotification>
    {
        public static readonly Guid UniqueId = Guid.Parse("88A17D65-52F7-4A10-9CF3-875C95B26EF3");

        public override Guid RefresherUniqueId => UniqueId;

        public override string Name => "CMSImport  cache refresher";

        public CacheRefresher(AppCaches appCaches, IEventAggregator eventAggregator, ICacheRefresherNotificationFactory factory)
            : base(appCaches, eventAggregator, factory)
        {
        }

        public override void RefreshAll()
        {
            base.AppCaches.RuntimeCache.Clear("ApplicationCache");
            base.RefreshAll();
        }

        public override void Refresh(int id)
        {
            Remove(id);
            base.Refresh(id);
        }

        public override void Remove(int id)
        {
            base.AppCaches.RuntimeCache.Clear("ApplicationCache");
            base.Remove(id);
        }
    }
}
