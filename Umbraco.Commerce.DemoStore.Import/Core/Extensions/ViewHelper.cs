namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions
{
    public class ViewHelper
    {
        public static string EnsureViewPluginsPath(string view, string umbracoPath)
        {
            if (view.AsString().EndsWith("html"))
            {
                return view;
            }
            return $"{umbracoPath.Replace("~", "")}/views/propertyeditors/{view}/{view}.html";
        }
    }
}
