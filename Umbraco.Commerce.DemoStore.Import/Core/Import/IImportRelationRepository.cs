using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Import
{
    public interface IImportRelationRepository
    {
        void Save(Guid umbracoId, ImportState state, string definitionAlias, string primaryKeyColumn, object primaryKeyValue);

        Guid? GetRelatedId(ImportState state, string definitionAlias, string primaryKeyColumn, object primaryKeyValue);

        void Delete(Guid umbracoId);

        void ClearUpdated(ImportState state, string primaryKeyColumn);

        IEnumerable<Guid> GetNotUpdatedIdsToDelete(ImportState state, string primaryKeyColumn);

        int Count(ImportState state);

        void RemoveOrphanNodes();
    }
}
