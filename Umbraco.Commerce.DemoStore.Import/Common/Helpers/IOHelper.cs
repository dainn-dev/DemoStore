using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Umbraco.Cms.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;
using Umbraco.Commerce.DemoStore.Import.Core.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class IOHelper : IIOHelper
    {
        private readonly IHostEnvironment _hostEnvironment;

        private readonly Umbraco.Cms.Core.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly IHttpContextAccessor _accessor;

        private readonly IHttpClientFactory _clientFactory;

        public string PluginUrl
        {
            get
            {
                string url = "/umbraco/cmsimport";
                return FormatUrl(url);
            }
        }

        public string UmbracoFolder
        {
            get
            {
                string text = FormatUrl("Umbraco").Trim();
                if (!text.EndsWith("/"))
                {
                    text = $"{text}/";
                }
                return text;
            }
        }

        public IOHelper(IHostEnvironment hostEnvironment, Umbraco.Cms.Core.Hosting.IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor, IHttpClientFactory clientFactory)
        {
            _hostEnvironment = hostEnvironment;
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
            _clientFactory = clientFactory;
        }

        public string FormatUrl(string url)
        {
            string text = _accessor?.HttpContext?.Request.GetDisplayUrl();
            if (text == null)
            {
                return url;
            }
            Uri uri = new Uri(text);
            return uri.Scheme + "://" + GetHostAndPort(uri) + url;
        }

        private string StripLastSlash(string url)
        {
            if (url.Length > 0 && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }

        public string GetApplicationUrl()
        {
            return StripLastSlash((_hostingEnvironment.ApplicationMainUrl.AbsoluteUri ?? "").Replace("/umbraco/", ""));
        }

        public string FormatPluginUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("/"))
            {
                url = url.Remove(0, 1);
            }
            return PluginUrl + "/" + url;
        }

        public string FormatUmbracoUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("/"))
            {
                url = url.Remove(0, 1);
            }
            return UmbracoFolder + "/" + url;
        }

        public string MapPathPluginUrl(string url)
        {
            return MapPath(Path.Combine("App_Plugins/" + PluginUrl, url));
        }

        public string MapPath(string path)
        {
            return HostEnvironmentExtensions.MapPathContentRoot(_hostEnvironment, path);
        }

        public string GetExtension(string url)
        {
            return Path.GetExtension(url).Remove(0, 1).ToLower();
        }

        public bool ParseRequest(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = (object p0, X509Certificate? p1 , X509Chain? p2, SslPolicyErrors p3) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpClient = _clientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(180.0);
            return httpClient.GetAsync(HttpUtility.HtmlDecode(url)).Result.StatusCode == HttpStatusCode.OK;
        }

        private string GetHostAndPort(Uri uri)
        {
            if (!IsDefaultPort(uri))
            {
                return $"{uri.Host}:{uri.Port}";
            }
            return uri.Host;
        }

        private bool IsDefaultPort(Uri uri)
        {
            if (uri.Port != 80)
            {
                return uri.Port == 443;
            }
            return true;
        }

        public string GetFileOnDisk(FileModel model)
        {
            return MapPath("~/umbraco/Data/TEMP/cmsimport/" + model.TempFolder + "/" + model.UniqueFileName);
        }

        public string MapPathWebRoot(string path)
        {
            return HostEnvironmentExtensions.MapPathContentRoot(_hostEnvironment, path);
        }
    }
}
