using Umbraco.Cms.Core.Services;

namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions
{
    public static class LocalizedTextServiceExtensions
    {
        public static string Localize(this ILocalizedTextService localizedTextService, string key)
        {
            string area = string.Empty;
            key = key.AsString().Replace("_", "/");
            if (key.Contains("/"))
            {
                string[] array = key.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (array.Length != 0)
                {
                    area = array[0];
                    key = array[1];
                }
            }
            string text = localizedTextService.Localize(area, key, Thread.CurrentThread.CurrentUICulture);
            if (text.StartsWith("[") && text.EndsWith("]"))
            {
                text = text.Substring(1, text.Length - 2);
            }
            return text;
        }

        public static string LocalizeString(this ILocalizedTextService localizedTextService, string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }
            if (s.StartsWith("[") && s.EndsWith("]"))
            {
                s = s.Substring(1, s.Length - 2);
            }
            return Localize(localizedTextService, s);
        }
    }
}
