namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ObjectNullExtensions
    {
        public static bool IsNullOrEmpty(this object value)
        {
            if (value != null && value != DBNull.Value)
            {
                return string.IsNullOrWhiteSpace(value.ToString());
            }
            return true;
        }
    }
}
