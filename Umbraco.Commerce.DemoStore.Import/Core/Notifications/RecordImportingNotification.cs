using System.Collections;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Notifications
{
    public class RecordImportingNotification<T> : CancelableObjectNotification<IContentBase> where T : IContentBase
    {
        public T Item { get; set; }

        public string ProviderAlias { get; set; }

        public ImportState ImportState { get; private set; }

        public object PrimaryKeyValue { get; private set; }

        public Hashtable Items { get; private set; }

        public RecordImportingNotification(T contentBase, ImportState state, object primaryKeyValue, Hashtable items, EventMessages messages)
            : base((IContentBase)contentBase, messages)
        {
            ImportState = state;
            ProviderAlias = state?.ImportProvider?.Alias;
            PrimaryKeyValue = primaryKeyValue;
            Items = items;
            Item = contentBase;
        }
    }
}
