using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public static class UrlHelper
    {
        public static Stream GetStream(IHttpClientFactory httpClientFactory, string url, bool useAuthentication, string userName, string password)
        {
            Stream result = null;
            try
            {
                HttpClient httpClient = httpClientFactory.CreateClient();
                if (useAuthentication)
                {
                    NetworkCredential networkCredential = new NetworkCredential(userName, password);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(networkCredential.UserName + ":" + networkCredential.Password)));
                }
                using Stream stream = httpClient.GetStreamAsync(url).Result;
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                result = memoryStream;
            }
            catch
            {
            }
            return result;
        }

        public static bool IsRelativeUrl(string url)
        {
            if (!url.Contains("://"))
            {
                return !IgnoreUrlStartingWith(url);
            }
            return false;
        }

        private static bool IgnoreUrlStartingWith(string url)
        {
            return new List<string>(new string[11]
            {
            "javascript", "mailto", "skype", "#", "?", "callto", "tel", "{{", "/remote.axd", "remote.axd",
            "data:image"
            }).Any((string item) => url.StartsWith(item, StringComparison.CurrentCultureIgnoreCase));
        }

        public static string GetQueryString(string url)
        {
            string[] array = url.AsString().Split('?');
            if (array.Count() != 2)
            {
                return string.Empty;
            }
            return $"?{array[1]}";
        }

        public static string RemoveQueryString(string url)
        {
            return url.AsString().Split('?')[0];
        }

        public static string EnsureFirstSlash(string url)
        {
            if (!url.StartsWith("/"))
            {
                return $"/{url}";
            }
            return url;
        }
    }
}
