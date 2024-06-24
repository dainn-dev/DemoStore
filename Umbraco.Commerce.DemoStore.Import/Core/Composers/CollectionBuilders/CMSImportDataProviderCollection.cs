using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class CMSImportDataProviderCollection : BuilderCollectionBase<DataProvider>
    {
        public CMSImportDataProviderCollection(Func<IEnumerable<DataProvider>> items)
            : base(items)
        {
        }
    }
}
