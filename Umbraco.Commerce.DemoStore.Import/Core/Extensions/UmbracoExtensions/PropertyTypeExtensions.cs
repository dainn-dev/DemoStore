using Umbraco.Cms.Core.Models;

namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions
{
    public static class PropertyTypeExtensions
    {
        public static bool IsElementProperty(this IPropertyType property)
        {
            if (!(property?.PropertyEditorAlias == "Umbraco.BlockList"))
            {
                return property?.PropertyEditorAlias == "Umbraco.BlockGrid";
            }
            return true;
        }
    }
}
