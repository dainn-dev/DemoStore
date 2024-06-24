using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Trees;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions.UmbracoExtensions
{
    public static class MenuItemCollectionExtensions
    {
        public static void AddMenuItem(this MenuItemCollection menuItemCollection, string alias, string name, string icon, bool addSeparator = false, bool asDialog = false, string route = null)
        {
            MenuItem menuItem = new MenuItem(alias, name)
            {
                SeparatorBefore = addSeparator,
                OpensDialog = asDialog,
                Icon = icon
            };
            if (!string.IsNullOrWhiteSpace(route))
            {
                menuItem.NavigateToRoute(route);
            }
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
