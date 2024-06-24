using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Parsers
{
    public interface IAdvancedPropertyParser
    {
        object Parse(object value, IContentBase importedItem, ImportPropertyInfo property, ImportOptions options);
    }
}
