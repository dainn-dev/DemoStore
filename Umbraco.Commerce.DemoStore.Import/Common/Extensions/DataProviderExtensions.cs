using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;
using Umbraco.Commerce.DemoStore.Import.Core.State;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Resolver;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class DataProviderExtensions
    {
        public static object ResolveLinks(this DataProvider dataProvider, IProviderOptions dataProviderOptions, ImportPropertyInfo propertyInfo, ImportState state, object value)
        {
            if (dataProvider is ILinkResolver linkResolver)
            {
                ResolverResult resolverResult = linkResolver.ResolveLinks(value, propertyInfo.PropertyEditorAlias, state);
                if (resolverResult.Status != 0)
                {
                    value = resolverResult.Value;
                }
            }
            return value;
        }
    }
}
