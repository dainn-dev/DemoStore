using System.Reflection;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class VersionInfoHelper : IVersionInfoHelper
    {
        public int GetMajorVersion()
        {
            return GetType().Assembly.GetName().Version?.Major ?? 99999;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty;
        }
    }

}
