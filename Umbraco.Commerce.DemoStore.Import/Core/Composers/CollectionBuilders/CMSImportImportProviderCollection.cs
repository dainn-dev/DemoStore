using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class CMSImportImportProviderCollection : BuilderCollectionBase<ImportProvider>
    {
        public CMSImportImportProviderCollection(Func<IEnumerable<ImportProvider>> items)
            : base(items)
        {
        }
    }
}
