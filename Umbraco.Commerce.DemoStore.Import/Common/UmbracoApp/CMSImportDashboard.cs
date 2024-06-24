using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Commerce.DemoStore.Import.Common.UmbracoApp
{
    [Weight(10)]
    public class CMSImportDashboard : IDashboard, IDashboardSlim
    {
        public string Alias => "cmsimportDashboard";

        public string[] Sections => new string[1] { "cmsimport" };

        public string View => "/App_Plugins/cmsimport/backoffice/cmsimport/cmsimport.html";

        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
    }

}
