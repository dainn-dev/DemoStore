using System.Collections;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Notifications
{
    public class RecordSkippedNotification<T> : INotification where T : IContentBase
    {
        public T Item { get; set; }

        public string ProviderAlias { get; set; }

        public ImportState ImportState { get; private set; }

        public object PrimaryKeyValue { get; private set; }

        public Hashtable Items { get; private set; }

        public RecordSkippedNotification(T contentItem, ImportState state, object primaryKeyValue, Hashtable items)
        {
            Item = contentItem;
            ImportState = state;
            ProviderAlias = state?.ImportProvider?.Alias;
            PrimaryKeyValue = primaryKeyValue;
            Items = items;
        }
    }
}
