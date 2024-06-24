using Microsoft.Extensions.Logging;
using System.Data;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Commerce.DemoStore.Import.Common.Model.DTO;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Import
{
    public class ImportRelationRepository : IImportRelationRepository
    {
        private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider _scopeProvider;

        private readonly ILogger<ImportRelationRepository> _logger;

        public ImportRelationRepository(Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider, ILogger<ImportRelationRepository> logger, IContentService contentService)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
        }

        public void Save(Guid umbracoId, ImportState state, string definitionAlias, string primaryKeyColumn, object primaryKeyValue)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope();
            string text = FormatDataSourceKey(state.DataProvider.Alias, definitionAlias, primaryKeyColumn, primaryKeyValue);
            CMSImportRelationDTO cMSImportRelationDTO = scope.Database.Query<CMSImportRelationDTO>("SELECT * From CMSImportRelation  WHERE  parentStateId = @0 AND  DatasourceKey = @1 and DefinitionAlias = @2", new object[3] { state.ParentStateId, text, definitionAlias }).FirstOrDefault() ?? new CMSImportRelationDTO
            {
                DatasourceKey = FormatDataSourceKey(state.DataProvider.Alias, definitionAlias, primaryKeyColumn, primaryKeyValue),
                DefinitionAlias = definitionAlias,
                ImportProvider = state.ImportProvider.Alias,
                UmbracoID = umbracoId
            };
            cMSImportRelationDTO.StateId = state.StateId;
            cMSImportRelationDTO.ParentStateId = state.ParentStateId;
            cMSImportRelationDTO.Updated = DateTime.Now;
            scope.Database.Save(cMSImportRelationDTO);
            scope.Complete();
        }

        public Guid? GetRelatedId(ImportState state, string definitionAlias, string primaryKeyColumn, object primaryKeyValue)
        {
            Guid? guid = null;
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope();
            guid = scope.Database.ExecuteScalar<Guid?>("SELECT UmbracoID From CMSImportRelation WHERE  parentStateId = @0 AND DatasourceKey = @1", new object[2]
            {
            state.ParentStateId,
            FormatDataSourceKey(state.DataProvider.Alias, definitionAlias, primaryKeyColumn, primaryKeyValue)
            });
            scope.Complete();
            return guid;
        }

        public void Delete(Guid umbracoId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope();
            scope.Database.Execute("DELETE From CMSImportRelation WHERE  UmbracoID  = @0", umbracoId);
            scope.Complete();
        }

        public void ClearUpdated(ImportState state, string primaryKeyColumn)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            string text = state.DataProvider.Alias + "%" + primaryKeyColumn + "%";
            scope.Database.Execute("UPDATE CMSImportRelation set updated = null WHERE  (StateId = @0 or StateId = @1) AND ImportProvider = @2 and DatasourceKey like @3", Guid.Empty, state.StateId, state.ImportProvider.Alias, text);
        }

        public IEnumerable<Guid> GetNotUpdatedIdsToDelete(ImportState state, string primaryKeyColumn)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            string text = state.DataProvider.Alias + "%" + primaryKeyColumn + "%";
            return from r in scope.Database.Fetch<CMSImportRelationDTO>("SELECT * FROM CMSImportRelation WHERE  updated IS null  AND (StateId = @0 or StateId = @1) AND ImportProvider = @2 and DatasourceKey like @3", new object[4]
                {
                Guid.Empty,
                state.StateId,
                state.ImportProvider.Alias,
                text
                })
                   select r.UmbracoID;
        }

        public int Count(ImportState state)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return scope.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM CMSImportRelation WHERE  ImportProvider  = @0", new object[1] { state.ImportProvider.Alias });
        }

        public void RemoveOrphanNodes()
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            scope.Database.Execute("DELETE from CMSImportRelation where CMSImportRelation.Id in (select CMSImportRelation.Id From CMSImportRelation Left Join umbracoNode On CMSImportRelation.UmbracoID = umbracoNode.uniqueId where umbracoNode.uniqueId is null)");
        }

        private string FormatDataSourceKey(string dataProvider, string definitionAlias, string dataKeyName, object dataKeyValue)
        {
            return $"{dataProvider}{definitionAlias}{dataKeyName}{dataKeyValue}".Trim();
        }
    }
}
