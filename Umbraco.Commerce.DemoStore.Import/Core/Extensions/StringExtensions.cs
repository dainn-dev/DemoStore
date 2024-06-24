using System.Text;

namespace Umbraco.Commerce.DemoStore.Import.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCsv(this List<int> items)
        {
            return items.Select((int item) => item.AsString()).ToList().ToCsv();
        }

        public static string ToCsv(this IEnumerable<string> items, string delimiter = ",")
        {
            StringBuilder stringBuilder = new StringBuilder();
            string arg = string.Empty;
            foreach (string item in items)
            {
                stringBuilder.AppendFormat("{0}{1}", arg, item);
                arg = delimiter;
            }
            return stringBuilder.ToString();
        }

        public static string AddCsv(this string s, string value)
        {
            List<string> list = s.ToList();
            list.Add(value);
            return list.ToCsv();
        }

        public static List<string> ToList(this string csv, char separator = ',')
        {
            string text = csv.AsString();
            if (!string.IsNullOrWhiteSpace(text))
            {
                return (from i in text.Split(new char[1] { separator }, StringSplitOptions.RemoveEmptyEntries)
                        select i.Trim()).ToList();
            }
            return new List<string>();
        }

        public static List<int> ToIntegerList(this string csv)
        {
            if (!string.IsNullOrWhiteSpace(csv))
            {
                return (from s in csv.Split(',')
                        select s.ToInt()).ToList();
            }
            return new List<int>();
        }

        public static List<int> ToIntegerList(this IEnumerable<string> items)
        {
            return items.Select((string s) => s.ToInt()).ToList();
        }

        public static Guid ToGuid(this string s)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(s, out result);
            return result;
        }

        public static char ToChar(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return '\0';
            }
            if (s.Length > 1)
            {
                throw new ArgumentException("ToChar requires a string of one");
            }
            char.TryParse(s, out var result);
            return result;
        }

        public static bool IsHttpUrl(this string s)
        {
            if (Uri.TryCreate(s, UriKind.Absolute, out Uri result))
            {
                if (!(result.Scheme == Uri.UriSchemeHttp))
                {
                    return result.Scheme == Uri.UriSchemeHttps;
                }
                return true;
            }
            return false;
        }

        public static T ToEnum<T>(this string s) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s, ignoreCase: true);
            }
            catch
            {
            }
            return default(T);
        }

        public static IEnumerable<string> RemoveEmptyEntries(this IEnumerable<string> lst)
        {
            return lst.Where((string i) => !string.IsNullOrWhiteSpace(i));
        }
    }

}
