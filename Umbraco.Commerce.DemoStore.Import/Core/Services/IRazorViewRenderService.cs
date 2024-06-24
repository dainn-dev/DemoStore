namespace Umbraco.Commerce.DemoStore.Import.Core.Services
{
    public interface IRazorViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
