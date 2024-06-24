namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class IENumerableExtensions
    {
        public static bool IsFirst<T>(this IEnumerable<T> items, T item)
        {
            T val = items.FirstOrDefault();
            if (val != null)
            {
                return item.Equals(val);
            }
            return false;
        }

        public static bool IsLast<T>(this IEnumerable<T> items, T item)
        {
            T val = items.LastOrDefault();
            if (val != null)
            {
                return item.Equals(val);
            }
            return false;
        }
    }
}
