using Umbraco.Commerce.DemoStore.Import.Core.Extensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Config
{
    public class MediaConfig
    {
        private string mediaImportLocation;

        public List<string> AllowedFileExtensions { get; set; }

        public List<string> AllowedDomains { get; set; }

        public string MediaImportLocation
        {
            get
            {
                return (mediaImportLocation.AsString().StartsWith("/") ? mediaImportLocation : ("/" + mediaImportLocation)).Replace("//", "/");
            }
            set
            {
                mediaImportLocation = value;
            }
        }

        public bool MediaImportKeepFolderStructure { get; set; }

        public string MediaImportImageTypeAlias { get; set; }

        public string MediaImportFileTypeAlias { get; set; }

        public string MediaImportFolderTypeAlias { get; set; }
    }
}
