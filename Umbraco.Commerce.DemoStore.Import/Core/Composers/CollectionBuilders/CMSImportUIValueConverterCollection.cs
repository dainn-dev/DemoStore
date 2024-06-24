using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Extentions.UI;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class CMSImportUIValueConverterCollection : BuilderCollectionBase<IFieldValueConverter>
    {
        public CMSImportUIValueConverterCollection(Func<IEnumerable<IFieldValueConverter>> items)
            : base(items)
        {
        }
    }
}
