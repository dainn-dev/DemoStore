using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public class CMSImportDataProviderCollectionBuilder : LazyCollectionBuilderBase<CMSImportDataProviderCollectionBuilder, CMSImportDataProviderCollection, DataProvider>
    {
        protected override CMSImportDataProviderCollectionBuilder This => this;

        protected override ServiceLifetime CollectionLifetime => ServiceLifetime.Transient;
    }
}
