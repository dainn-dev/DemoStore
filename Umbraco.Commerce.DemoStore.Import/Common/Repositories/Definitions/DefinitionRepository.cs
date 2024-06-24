using System.Data;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers;
using Umbraco.Commerce.DemoStore.Import.Common.Model.DTO;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask.Models;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions
{
    public class DefinitionRepository : IDefinitionRepository
    {
        private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider _scopeProvider;

        private readonly IScheduledTaskRepository _scheduledTaskRepository;

        private readonly IImportProviderRepository _importProviderRepository;

        public DefinitionRepository(Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider, IScheduledTaskRepository scheduledTaskRepository, IImportProviderRepository importProviderRepository)
        {
            _scopeProvider = scopeProvider;
            _scheduledTaskRepository = scheduledTaskRepository;
            _importProviderRepository = importProviderRepository;
        }

        public IEnumerable<SimpleDefinitionModel> GetAll()
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return from s in scope.Database.Query<CMSImportStateDTO>("SELECT Id, UniqueIdentifier, Name, Parent, ImportProvider FROM CMSImportState ", Array.Empty<object>())
                   select new SimpleDefinitionModel
                   {
                       Name = s.Name,
                       ImportProvider = s.ImportProvider,
                       Id = s.UniqueIdentifier,
                       ParentId = s.Parent,
                       Icon = (_importProviderRepository.Single(s.ImportProvider)?.Icon ?? "icon-umb-developer")
                   };
        }

        public IEnumerable<SimpleDefinitionModel> Children(string importProvider)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return from s in scope.Database.Query<CMSImportStateDTO>("SELECT Id, UniqueIdentifier, Name, Parent, ImportProvider FROM CMSImportState  WHERE importProvider = @0 AND Parent = @1", new object[2]
                {
                importProvider,
                Guid.Empty
                })
                   select new SimpleDefinitionModel
                   {
                       Name = s.Name,
                       ImportProvider = s.ImportProvider,
                       Id = s.UniqueIdentifier,
                       ParentId = s.Parent,
                       Icon = (_importProviderRepository.Single(s.ImportProvider)?.Icon ?? "icon-umb-developer")
                   };
        }

        public IEnumerable<SimpleDefinitionModel> Children(Guid parentId, bool showRoot = false)
        {
            if (parentId == Guid.Empty && !showRoot)
            {
                return new List<SimpleDefinitionModel>();
            }
            return GetChildren(parentId);
        }

        public void Save(StateDefinitionModel model)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope();
            CMSImportStateDTO cMSImportStateDTO = scope.Database.FirstOrDefault<CMSImportStateDTO>("SELECT Id, UniqueIdentifier, Name, Parent, ImportProvider FROM CMSImportState  WHERE UniqueIdentifier = @0", new object[1] { model.Id }) ?? new CMSImportStateDTO
            {
                ImportProvider = model.ImportProvider,
                Parent = model.ParentId,
                UniqueIdentifier = model.Id
            };
            cMSImportStateDTO.Name = EnsureUniqueName(model.Id, model.ParentId, model.Name, null);
            cMSImportStateDTO.ImportState = SerializeHelper.SerializeBase64(model.ImportState);
            scope.Database.Save(cMSImportStateDTO);
            scope.Complete();
        }

        private string EnsureUniqueName(Guid id, Guid parentId, string name, int? suffix)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            string text = $"{name}{suffix}";
            CMSImportStateDTO cMSImportStateDTO = scope.Database.FirstOrDefault<CMSImportStateDTO>("SELECT UniqueIdentifier FROM  CMSImportState WHERE Parent = @0 and Name = @1", new object[2] { parentId, text });
            if (cMSImportStateDTO != null && !cMSImportStateDTO.UniqueIdentifier.Equals(id))
            {
                return EnsureUniqueName(id, parentId, name, (suffix ?? 0) + 1);
            }
            return text;
        }

        private IEnumerable<SimpleDefinitionModel> GetChildren(Guid? parentId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return from s in scope.Database.Query<CMSImportStateDTO>("SELECT Id, UniqueIdentifier, Name, Parent, ImportProvider FROM CMSImportState  WHERE parent = @0", new object[1] { parentId })
                   select new SimpleDefinitionModel
                   {
                       Name = s.Name,
                       ImportProvider = s.ImportProvider,
                       Id = s.UniqueIdentifier,
                       ParentId = s.Parent,
                       Icon = (_importProviderRepository.Single(s.ImportProvider)?.Icon ?? "icon-umb-developer")
                   };
        }

        public StateDefinitionModel Single(Guid id)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            StateDefinitionModel stateDefinitionModel = (from s in scope.Database.Query<CMSImportStateDTO>("SELECT * FROM CMSImportState  WHERE UniqueIdentifier = @0", new object[1] { id })
                                                         select new StateDefinitionModel
                                                         {
                                                             Name = s.Name,
                                                             ImportProvider = s.ImportProvider,
                                                             Id = s.UniqueIdentifier,
                                                             ParentId = s.Parent,
                                                             ImportState = SerializeHelper.DeserializeBase64<ImportState>(s.ImportState)
                                                         }).FirstOrDefault();
            if (stateDefinitionModel != null)
            {
                stateDefinitionModel.AllowChildDefinition = stateDefinitionModel.ImportState.ImportProvider.SupportedChild == stateDefinitionModel.ImportProvider;
                stateDefinitionModel.Icon = stateDefinitionModel.ImportState.ImportProvider.Icon;
                stateDefinitionModel.ImportState.ParentStateId = stateDefinitionModel.ParentId;
            }
            return stateDefinitionModel;
        }

        public StateDefinitionModel GetParent(Guid id)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            Guid? guid = scope.Database.ExecuteScalar<Guid?>("SELECT Parent FROM CMSImportState WHERE UniqueIdentifier = @0", new object[1] { id });
            return (from s in scope.Database.Query<CMSImportStateDTO>("SELECT * FROM CMSImportState  WHERE UniqueIdentifier = @0", new object[1] { guid })
                    select new StateDefinitionModel
                    {
                        Name = s.Name,
                        ImportProvider = s.ImportProvider,
                        Id = s.UniqueIdentifier,
                        ParentId = s.Parent,
                        ImportState = SerializeHelper.DeserializeBase64<ImportState>(s.ImportState)
                    }).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            foreach (ScheduledTaskModel item in (from s in _scheduledTaskRepository.GetAll()
                                                 where s.StateId == id
                                                 select s).ToList())
            {
                _scheduledTaskRepository.Delete(item.ScheduledTaskId);
            }
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scopeProvider.CreateScope();
            scope.Database.Execute("DELETE FROM CMSImportState WHERE Parent = @0", id);
            scope.Database.Execute("DELETE FROM CMSImportState WHERE UniqueIdentifier = @0", id);
            scope.Complete();
        }
    }
}
