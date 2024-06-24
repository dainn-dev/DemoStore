using Umbraco.Commerce.DemoStore.Import.Core.Models.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;

namespace Umbraco.Commerce.DemoStore.Import.Core.Import
{
    public interface IMediaImport
    {
        IEnumerable<Guid> Import(string relativeUrl, Guid imageParentId, Guid fileParentId, int importUserId, bool breakWhenFileNotExists, IProviderOptions dataProviderOptions = null);

        MediaRelationInfo GetRelatedMediaItem(string relativeUrl);

        void DeleteRelation(Guid umbracoId);

        string FormatRelativeUrl(string url);
    }
}
