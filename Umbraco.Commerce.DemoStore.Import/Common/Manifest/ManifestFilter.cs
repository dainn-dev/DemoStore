using Umbraco.Cms.Core.Manifest;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;

namespace Umbraco.Commerce.DemoStore.Import.Common.Manifest
{
    public class ManifestFilter : IManifestFilter
    {
        private readonly IVersionInfoHelper _versionInfoHelper;

        public ManifestFilter(IVersionInfoHelper versionInfoHelper)
        {
            _versionInfoHelper = versionInfoHelper;
        }

        public void Filter(List<PackageManifest> manifests)
        {
            manifests.Add(new PackageManifest
            {
                PackageName = "CMSImport",
                Version = _versionInfoHelper.GetVersion(),
                AllowPackageTelemetry = true,
                BundleOptions = BundleOptions.Independent,
                Scripts = new string[1] { "/App_Plugins/cmsimport/backoffice/cmsimport/cmsimport.js" },
                Stylesheets = new string[1] { "/App_Plugins/cmsimport/css/cmsimport.css" }
            });
        }
    }
}
