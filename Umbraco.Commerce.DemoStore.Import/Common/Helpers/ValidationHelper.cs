using System.Text.RegularExpressions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string emailAddress)
        {
            return new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").Match(emailAddress).Success;
        }
    }
}
