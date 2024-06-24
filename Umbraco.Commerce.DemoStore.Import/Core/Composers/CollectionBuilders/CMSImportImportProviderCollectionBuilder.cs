using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public class CMSImportImportProviderCollectionBuilder : LazyCollectionBuilderBase<CMSImportImportProviderCollectionBuilder, CMSImportImportProviderCollection, ImportProvider>
    {
        protected override CMSImportImportProviderCollectionBuilder This => this;

        protected override ServiceLifetime CollectionLifetime => ServiceLifetime.Transient;
    }
}
