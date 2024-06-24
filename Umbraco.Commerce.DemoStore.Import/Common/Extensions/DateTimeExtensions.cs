namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int TotalSeconds(this DateTime dt)
        {
            return Convert.ToInt32(new TimeSpan(dt.Ticks).TotalSeconds);
        }
    }
}
