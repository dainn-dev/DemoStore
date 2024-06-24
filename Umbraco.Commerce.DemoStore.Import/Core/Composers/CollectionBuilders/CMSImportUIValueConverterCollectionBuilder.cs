using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Extentions.UI;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public class CMSImportUIValueConverterCollectionBuilder : LazyCollectionBuilderBase<CMSImportUIValueConverterCollectionBuilder, CMSImportUIValueConverterCollection, IFieldValueConverter>
    {
        protected override CMSImportUIValueConverterCollectionBuilder This => this;

        protected override ServiceLifetime CollectionLifetime => ServiceLifetime.Transient;
    }
}
