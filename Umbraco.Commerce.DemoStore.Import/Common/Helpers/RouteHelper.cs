namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public static class RouteHelper
    {
        public static string RenderAngularRoute(string viewLocation)
        {
            return RenderAngularRoute(viewLocation, string.Empty);
        }

        public static string RenderAngularRoute(string viewLocation, string id)
        {
            return "/cmsimport/cmsimport/" + viewLocation + "/" + id;
        }
    }
}
