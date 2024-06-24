using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class CMSImportFieldProviderCollection : BuilderCollectionBase<IFieldProvider>
    {
        public CMSImportFieldProviderCollection(Func<IEnumerable<IFieldProvider>> items)
            : base(items)
        {
        }
    }
}
