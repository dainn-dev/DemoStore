using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Commerce.DemoStore.Import.Core.Services;
using Microsoft.AspNetCore.Routing;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services
{

    public class RazorViewRenderService : IRazorViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;

        private readonly ITempDataProvider _tempDataProvider;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RazorViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceScopeFactory serviceScopeFactory)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            ActionContext actionContext = new ActionContext(new DefaultHttpContext
            {
                RequestServices = scope.ServiceProvider
            }, new RouteData(), new ActionDescriptor());
            using StringWriter sw = new StringWriter();
            ViewEngineResult view = _razorViewEngine.GetView("~/", viewName, isMainPage: false);
            if (view.View == null)
            {
                throw new ArgumentNullException(viewName + " does not match any available view");
            }
            ViewDataDictionary viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            ViewContext context = new ViewContext(actionContext, view.View, viewData, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());
            await view.View.RenderAsync(context);
            return sw.ToString();
        }
    }

}
