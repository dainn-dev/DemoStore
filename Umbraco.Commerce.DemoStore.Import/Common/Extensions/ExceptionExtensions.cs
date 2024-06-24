using System.Text;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetErrorMessage(this Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddExceptions(ex, stringBuilder);
            return stringBuilder.ToString();
        }

        private static void AddExceptions(Exception ex, StringBuilder sb)
        {
            StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(1, 1, sb);
            handler.AppendFormatted(ex.Message);
            handler.AppendLiteral(" ");
            sb.Append(ref handler);
            if (ex.InnerException != null)
            {
                AddExceptions(ex.InnerException, sb);
            }
        }
    }
}
