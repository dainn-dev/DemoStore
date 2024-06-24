using Newtonsoft.Json;
namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string AsString(this object objectToCast)
        {
            return $"{objectToCast}";
        }

        public static string AsString(this object s, string defaultValue)
        {
            string text = $"{s}";
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            return defaultValue;
        }

        public static string AsLowerString(this object objectToCast)
        {
            return objectToCast.AsString().ToLower();
        }

        public static int ToInt(this object objectToCast)
        {
            int.TryParse(objectToCast.AsString(), out var result);
            return result;
        }

        public static Guid ToGuid(this object o)
        {
            return o.AsString().ToGuid();
        }

        public static int? ToNullableInt(this object o)
        {
            if (o == null)
            {
                return null;
            }
            if (int.TryParse(o.AsString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static decimal? ToNullableDecimal(this object o)
        {
            if (o == null)
            {
                return null;
            }
            if (decimal.TryParse(o.AsString(), out var result))
            {
                return result;
            }
            return null;
        }

        public static bool? ToNullableBoolean(this object o)
        {
            return o?.ToBoolean(defaultValue: false);
        }

        public static bool ToBoolean(this object o, bool defaultValue)
        {
            bool.TryParse(o.AsString(), out defaultValue);
            return defaultValue;
        }

        public static T CastTo<T>(this object o) where T : class
        {
            return o as T;
        }

        public static T ToEnum<T>(this object o) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum ");
            }
            return (T)Enum.Parse(typeof(T), o.AsString(), ignoreCase: true);
        }

        public static bool ToBool(this object o)
        {
            bool.TryParse(o.AsString(), out var result);
            return result;
        }

        public static DateTime ToDate(this object o, DateTime? defaultValue = null)
        {
            DateTime result = defaultValue.GetValueOrDefault(DateTime.MinValue);
            DateTime.TryParse(o.AsString(), out result);
            return result;
        }

        public static T ConvertUsingJson<T>(this object o) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(o));
        }
    }
}
