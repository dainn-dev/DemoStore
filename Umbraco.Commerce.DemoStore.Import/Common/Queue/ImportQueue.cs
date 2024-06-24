using Microsoft.Extensions.Logging;
using Umbraco.Commerce.DemoStore.Import.Common.Extensions;
using Umbraco.Commerce.DemoStore.Import.Common.Model;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Common.Queue
{
    public class ImportQueue : IImportQueue
    {
        private readonly Dictionary<ImportStatus, ImportState> _queue = new Dictionary<ImportStatus, ImportState>();

        private readonly object _lock = new object();

        private readonly IDefinitionRepository _definitionRepository;

        private readonly ILogger<ImportQueue> _loggingProvider;

        private readonly IImportRelationRepository _importRelationRepository;

        private readonly IImportProviderRepository _importProviderRepository;

        private readonly IDataproviderRepository _dataproviderRepository;

        public Dictionary<ImportStatus, ImportState> Queue => _queue;

        public ImportQueue(IDefinitionRepository definitionRepository, ILogger<ImportQueue> loggingProvider, IImportRelationRepository importRelationRepository, IImportProviderRepository importProviderRepository, IDataproviderRepository dataproviderRepository)
        {
            _definitionRepository = definitionRepository;
            _loggingProvider = loggingProvider;
            _importRelationRepository = importRelationRepository;
            _importProviderRepository = importProviderRepository;
            _dataproviderRepository = dataproviderRepository;
        }

        public void AddToQueue(ImportStatus status, ImportState state)
        {
            Queue.Add(status, state);
        }

        public ImportResult Start(ImportStatus status, bool scheduled = false)
        {
            KeyValuePair<ImportStatus, ImportState> keyValuePair = Queue.FirstOrDefault((KeyValuePair<ImportStatus, ImportState> s) => s.Key.ImportId.Equals(status.ImportId));
            ImportResult importResult = new ImportResult();
            try
            {
                DateTime now = DateTime.Now;
                _importRelationRepository.RemoveOrphanNodes();
                ImportProvider importProvider = _importProviderRepository.Single(keyValuePair.Value.ImportProvider.Alias);
                importProvider.FireBulkImporting(keyValuePair.Value);
                ImportCurrentAndChildren(keyValuePair.Value, status, importResult, scheduled);
                importProvider.FireBulkImported(keyValuePair.Value);
                importResult.Duration = new DateTime(DateTime.Now.Subtract(now).Ticks);
            }
            catch (Exception ex)
            {
                _loggingProvider.LogError(ex, "Error during import can't contine'");
                importResult.Errors.Add("Error during import, can't continue " + ex.Message);
            }
            importResult.Canceled = keyValuePair.Key.Canceled;
            keyValuePair.Key.Result = importResult;
            keyValuePair.Key.Finished = true;
            return importResult;
        }

        public void ImportCurrentAndChildren(ImportState state, ImportStatus status, ImportResult result, bool scheduled)
        {
            ImportProvider importProvider = _importProviderRepository.Single(state.ImportProvider.Alias);
            result.Add(scheduled ? importProvider.Import(state) : state.ImportProvider.Import(state));
            foreach (SimpleDefinitionModel item in _definitionRepository.Children(state.StateId))
            {
                if (!importProvider.CancelImport && !result.Errors.Any())
                {
                    ImportState importState = _definitionRepository.Single(item.Id).ImportState;
                    importState.DataProvider = _dataproviderRepository.Single(importState.DataProvider.Alias);
                    importState.ImportProvider = _importProviderRepository.Single(importState.ImportProvider.Alias);
                    Queue.FirstOrDefault((KeyValuePair<ImportStatus, ImportState> s) => s.Key.ImportId.Equals(status.ImportId)).Value.ImportProvider = importState.ImportProvider;
                    ImportCurrentAndChildren(importState, status, result, scheduled);
                }
            }
        }

        public ImportStatus GetProgress(ImportStatus status)
        {
            ImportStatus importStatus = status;
            KeyValuePair<ImportStatus, ImportState> keyValuePair = Queue.FirstOrDefault((KeyValuePair<ImportStatus, ImportState> s) => s.Key.ImportId.Equals(status.ImportId));
            ImportProvider importProvider = keyValuePair.Value.ImportProvider;
            status = keyValuePair.Key;
            if (importStatus.Canceled)
            {
                keyValuePair.Key.Canceled = true;
                importProvider.CancelImport = true;
            }
            status.StatusMessage = importProvider.StatusMessage;
            return status;
        }
    }
}
