using Umbraco.Commerce.DemoStore.Import.Core.Models.Resolver;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Import
{
    public interface ILinkResolver
    {
        ResolverResult ResolveLinks(object value, string propertyEditorAlias, ImportState state);
    }
}
