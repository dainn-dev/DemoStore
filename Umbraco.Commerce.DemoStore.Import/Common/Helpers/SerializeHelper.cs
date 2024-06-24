using Newtonsoft.Json;
using System.Text;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class SerializeHelper
    {
        public static string SerializeBase64(object o)
        {
            string s = JsonConvert.SerializeObject(o, GetSerializerSettings());
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }

        public static T DeserializeBase64<T>(string s) where T : class
        {
            byte[] bytes = Convert.FromBase64String(s);
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), GetSerializerSettings());
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
