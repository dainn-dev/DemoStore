using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ContentServiceExtensions
    {
        public static void Save(this IContentService contentService, IContent content, bool autoPublish, int userId)
        {
            if (autoPublish)
            {
                contentService.SaveAndPublish(content, "*", userId);
            }
            else
            {
                contentService.Save(content, userId);
            }
        }
    }
}
