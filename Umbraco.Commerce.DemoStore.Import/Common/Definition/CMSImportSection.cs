
using Umbraco.Cms.Core.Sections;

namespace Umbraco.Commerce.DemoStore.Import.Common.Definition
{
    public class CMSImportSection : ISection
    {
        public string Alias => "cmsimport";

        public string Name => "Import";
    }
}
