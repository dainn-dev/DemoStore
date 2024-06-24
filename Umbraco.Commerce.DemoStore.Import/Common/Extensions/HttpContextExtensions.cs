using Microsoft.AspNetCore.Http;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GenerateAbsoluteUrl(this HttpContext httpContext, string relativeUrl)
        {
            string scheme = httpContext.Request.Scheme;
            string hostAndPort = GetHostAndPort(httpContext.Request);
            return $"{scheme}://{hostAndPort}/{relativeUrl}";
        }

        private static string GetHostAndPort(this HttpRequest request)
        {
            if (!IsDefaultPort(request))
            {
                return $"{request.Host.Host}:{request.Host.Port}";
            }
            return request.Host.Host;
        }

        private static bool IsDefaultPort(this HttpRequest request)
        {
            if (request.Host.Port != 80)
            {
                return request.Host.Port == 443;
            }
            return true;
        }
    }
}
