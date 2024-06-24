using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ConfirmOptionProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class ConfirmOptionProviderCollection : BuilderCollectionBase<IConfirmOptionProvider>
    {
        public ConfirmOptionProviderCollection(Func<IEnumerable<IConfirmOptionProvider>> items)
            : base(items)
        {
        }
    }
}
