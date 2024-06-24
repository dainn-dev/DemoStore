using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class MediaExtensions
    {
        public static void SetFile(this IContentBase mediaItem, MediaFileManager mediaFileManager, string filePath, string propertyAlias = "umbracoFile", string culture = null)
        {
            string fileName = Path.GetFileName(filePath);
            using FileStream filestream = System.IO.File.Open(filePath, FileMode.Open);
            string path = mediaFileManager.StoreFile(mediaItem, mediaItem.Properties.FirstOrDefault((IProperty p) => p.Alias == propertyAlias)?.PropertyType, fileName, filestream, null);
            mediaItem.Properties.FirstOrDefault((IProperty p) => p.Alias == propertyAlias)?.SetValue(mediaFileManager.FileSystem.GetUrl(path), culture);
        }
    }
}
