using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public class CMSImportFieldProviderCollectionBuilder : LazyCollectionBuilderBase<CMSImportFieldProviderCollectionBuilder, CMSImportFieldProviderCollection, IFieldProvider>
    {
        protected override CMSImportFieldProviderCollectionBuilder This => this;

        protected override ServiceLifetime CollectionLifetime => ServiceLifetime.Transient;
    }

}
