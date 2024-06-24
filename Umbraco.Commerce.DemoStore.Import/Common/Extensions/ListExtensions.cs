namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ListExtensions
    {
        public static void AddUniqueItem<T>(this List<T> lst, T item)
        {
            if (!lst.Contains(item))
            {
                lst.Add(item);
            }
        }
    }
}
