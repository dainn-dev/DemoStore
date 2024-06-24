using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Commerce.DemoStore.Import.Common.Config;
using Umbraco.Commerce.DemoStore.Import.Common.Model.DTO;
using Umbraco.Commerce.DemoStore.Import.Common.Model;
using Umbraco.Commerce.DemoStore.Import.Common.Queue;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions.Models;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.Definitions;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Mail;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail.Model;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using System.Data;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask
{
    public class ScheduledTaskRepository : IScheduledTaskRepository
    {
        private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider _scope;

        private readonly ILogger<ScheduledTaskRepository> _logging;

        private readonly Lazy<IImportQueue> _importQueue;

        private readonly Lazy<IDefinitionRepository> _definitionRepository;

        private readonly IImportProviderRepository _importProviderRepository;

        private readonly IDataproviderRepository _dataproviderRepository;

        private readonly IRazorMailer _razorMailer;

        private readonly CmsImportConfig _config;

        public ScheduledTaskRepository(Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider, ILogger<ScheduledTaskRepository> logging, Lazy<IImportQueue> importQueue, Lazy<IDefinitionRepository> definitionRepository, IImportProviderRepository importProviderRepository, IDataproviderRepository dataproviderRepository, IRazorMailer razorMailer, IOptions<CmsImportConfig> config)
        {
            _scope = scopeProvider;
            _logging = logging;
            _importQueue = importQueue;
            _definitionRepository = definitionRepository;
            _importProviderRepository = importProviderRepository;
            _dataproviderRepository = dataproviderRepository;
            _razorMailer = razorMailer;
            _config = config.Value;
        }

        public IEnumerable<ScheduledTaskModel> GetAll()
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return from dto in scope.Database.Query<CMSImportScheduledTaskDefinitionDTO>("SELECT * FROM CMSImportScheduledTaskDefinition ORDER BY ScheduledTaskName", Array.Empty<object>())
                   select ConvertToModel(dto);
        }

        public ScheduledTaskModel Single(Guid scheduledTaskId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return ConvertToModel(scope.Database.FirstOrDefault<CMSImportScheduledTaskDefinitionDTO>("SELECT * FROM CMSImportScheduledTaskDefinition Where ScheduledTaskId = @0", new object[1] { scheduledTaskId }));
        }

        public void Delete(Guid scheduledTaskId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope();
            scope.Database.Execute("DELETE FROM CMSImportScheduledTaskResult WHERE ScheduledTaskId = @0", scheduledTaskId);
            scope.Database.Execute("DELETE FROM CMSImportScheduledTaskDefinition WHERE ScheduledTaskId = @0", scheduledTaskId);
            scope.Complete();
        }

        public void Save(ScheduledTaskModel model)
        {
            model.NextRunTime = CalculateNextRunTime(model);
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope();
            CMSImportScheduledTaskDefinitionDTO cMSImportScheduledTaskDefinitionDTO = scope.Database.FirstOrDefault<CMSImportScheduledTaskDefinitionDTO>("SELECT * FROM CMSImportScheduledTaskDefinition  WHERE ScheduledTaskId = @0", new object[1] { model.ScheduledTaskId }) ?? new CMSImportScheduledTaskDefinitionDTO();
            if (MustUpdateNextRuntime(model, cMSImportScheduledTaskDefinitionDTO))
            {
                cMSImportScheduledTaskDefinitionDTO.NextRunTime = model.NextRunTime;
            }
            cMSImportScheduledTaskDefinitionDTO.StateId = model.StateId;
            cMSImportScheduledTaskDefinitionDTO.Interval = model.Interval;
            cMSImportScheduledTaskDefinitionDTO.IsInterval = model.ScheduledTaskType == ScheduledTaskType.Interval;
            cMSImportScheduledTaskDefinitionDTO.NotificationAddress = model.NotificationAddress;
            cMSImportScheduledTaskDefinitionDTO.Days = model.Days.ToCsv();
            cMSImportScheduledTaskDefinitionDTO.Hour = model.Hour;
            cMSImportScheduledTaskDefinitionDTO.Minute = model.Minute;
            cMSImportScheduledTaskDefinitionDTO.ScheduledTaskId = model.ScheduledTaskId;
            cMSImportScheduledTaskDefinitionDTO.ScheduledTaskName = model.ScheduledTaskName;
            cMSImportScheduledTaskDefinitionDTO.UserId = model.UserId;
            cMSImportScheduledTaskDefinitionDTO.InProgress = false;
            scope.Database.Save(cMSImportScheduledTaskDefinitionDTO);
            scope.Complete();
            model.NextRunTime = cMSImportScheduledTaskDefinitionDTO.NextRunTime;
        }

        private ScheduledTaskModel ConvertToModel(CMSImportScheduledTaskDefinitionDTO dto)
        {
            return new ScheduledTaskModel
            {
                Days = dto.Days.ToIntegerList(),
                Hour = dto.Hour,
                StateId = dto.StateId,
                Interval = dto.Interval,
                ScheduledTaskType = (dto.IsInterval ? ScheduledTaskType.Interval : ScheduledTaskType.Daily),
                NotificationAddress = dto.NotificationAddress,
                ScheduledTaskId = dto.ScheduledTaskId,
                ScheduledTaskName = dto.ScheduledTaskName,
                UserId = dto.UserId,
                Minute = dto.Minute,
                NextRunTime = dto.NextRunTime,
                LastTimeExecuted = dto.LastTimeExecuted,
                HasScheduledTaskResult = HasLogEntries(dto.ScheduledTaskId)
            };
        }

        private bool HasLogEntries(Guid scheduledTaskId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return scope.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM CMSImportScheduledTaskResult where ScheduledTaskId = @0", new object[1] { scheduledTaskId }) > 0;
        }

        private DateTime CalculateNextRunTime(ScheduledTaskModel model)
        {
            if (model.ScheduledTaskType == ScheduledTaskType.Interval)
            {
                return DateTime.Now.AddMinutes(model.Interval);
            }
            return GetNextWeekyDateTime(model.Days, model.Hour, model.Minute);
        }

        private bool MustUpdateNextRuntime(ScheduledTaskModel model, CMSImportScheduledTaskDefinitionDTO dto)
        {
            if (!dto.NextRunTime.HasValue)
            {
                return true;
            }
            if (model.ScheduledTaskType == ScheduledTaskType.Interval)
            {
                return model.Interval != dto.Interval;
            }
            if (model.Hour != dto.Hour || model.Minute != dto.Minute)
            {
                return true;
            }
            if (!model.Days.SequenceEqual(dto.Days.ToIntegerList()))
            {
                return true;
            }
            return false;
        }

        public DateTime GetNextWeekyDateTime(IEnumerable<int> days, int hour, int minute)
        {
            DateTime now = DateTime.Now;
            DateTime result = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            for (int i = 0; i < 10; i++)
            {
                DateTime tmp = result.AddDays(i);
                if (days.Any((int d) => d == GetDayOfWeekAsNumericString(tmp)) && tmp.Ticks >= now.Ticks)
                {
                    return tmp;
                }
            }
            return result;
        }

        private int GetDayOfWeekAsNumericString(DateTime dt)
        {
            return (int)(dt.DayOfWeek + 1);
        }

        public void TryExecuteNextImportTask()
        {
            CMSImportScheduledTaskDefinitionDTO cMSImportScheduledTaskDefinitionDTO = null;
            ImportStatus importStatus = null;
            try
            {
                if (ScheduledTaskInProgress())
                {
                    _logging.LogDebug("An(other) import is already running. Check back later");
                    return;
                }
                using (Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true))
                {
                    _logging.LogDebug("Check database for tasks to execute");
                    cMSImportScheduledTaskDefinitionDTO = scope.Database.FirstOrDefault<CMSImportScheduledTaskDefinitionDTO>("SELECT *  FROM CMSImportScheduledTaskDefinition WHERE InProgress = 0  AND NextRunTime < @0 Order By NextRuntime", new object[1] { DateTime.Now });
                }
                if (cMSImportScheduledTaskDefinitionDTO != null)
                {
                    _logging.LogInformation("Execute Import definition " + cMSImportScheduledTaskDefinitionDTO.ScheduledTaskName);
                    SetInProgressFlag(cMSImportScheduledTaskDefinitionDTO, inProgress: true);
                    importStatus = new ImportStatus
                    {
                        ImportId = Guid.NewGuid(),
                        StatusMessage = "Starting ScheduledImport"
                    };
                    StateDefinitionModel stateDefinitionModel = _definitionRepository.Value.Single(cMSImportScheduledTaskDefinitionDTO.StateId);
                    stateDefinitionModel.ImportState.ImportProvider = _importProviderRepository.Single(stateDefinitionModel.ImportState.ImportProvider.Alias);
                    stateDefinitionModel.ImportState.DataProvider = _dataproviderRepository.Single(stateDefinitionModel.ImportState.DataProvider.Alias);
                    _importQueue?.Value.AddToQueue(importStatus, stateDefinitionModel.ImportState);
                    _importQueue?.Value.Start(importStatus, scheduled: true);
                }
            }
            catch (Exception exception)
            {
                _logging.LogError(exception, "Error during scheduled import");
            }
            if (cMSImportScheduledTaskDefinitionDTO != null)
            {
                WriteImportLogResultRecord(importStatus, cMSImportScheduledTaskDefinitionDTO.ScheduledTaskId);
                SendResultMail(importStatus, cMSImportScheduledTaskDefinitionDTO);
                SetNextRuntime(cMSImportScheduledTaskDefinitionDTO);
                SetInProgressFlag(cMSImportScheduledTaskDefinitionDTO, inProgress: false);
            }
        }

        public IEnumerable<ScheduledTaskResult> GetLog(Guid scheduledTaskId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return (from dto in scope.Database.Query<CMSImportScheduledTaskResultDTO>("SELECT ScheduledTaskId,Success,Executed,Duration,Errors FROM CMSImportScheduledTaskResult where ScheduledTaskId = @0 ORDER By Executed DESC", new object[1] { scheduledTaskId }).Take(100)
                    select new ScheduledTaskResult
                    {
                        Errors = dto.Errors,
                        Duration = dto.Duration,
                        Success = dto.Success,
                        Executed = dto.Executed
                    }).ToList();
        }

        public void ClearLog(Guid scheduledTaskId)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            scope.Database.Execute("DELETE FROM CMSImportScheduledTaskResult where ScheduledTaskId = @0 ", scheduledTaskId);
        }

        private void SendResultMail(ImportStatus importStatus, CMSImportScheduledTaskDefinitionDTO model)
        {
            try
            {
                _logging.LogDebug("Scheduled Import finished, Send Result email");
                ScheduledTaskMail scheduledTaskMail = new ScheduledTaskMail
                {
                    RecordCount = importStatus.Result.RecordsInDataSource,
                    RecordsAdded = importStatus.Result.RecordsAdded,
                    RecordsDeleted = importStatus.Result.RecordsDeleted,
                    RecordsSkipped = importStatus.Result.RecordsSkipped,
                    RecordsUpdated = importStatus.Result.RecordsUpdated,
                    TaskName = model.ScheduledTaskName
                };
                scheduledTaskMail.ErrorMessages.AddRange(importStatus.Result.Errors);
                EmailConfiguration config = new EmailConfiguration
                {
                    FromAddress = _config.ScheduledTaskMailConfig.FromAddress,
                    FromName = _config.ScheduledTaskMailConfig.FromAddress,
                    Subject = _config.ScheduledTaskMailConfig.Subject,
                    Template = _config.ScheduledTaskMailConfig.ViewLocation
                };
                foreach (string item in Umbraco.Extensions.StringExtensions.ToDelimitedList(model.NotificationAddress))
                {
                    _razorMailer.Send(scheduledTaskMail, config, item);
                }
            }
            catch (Exception exception)
            {
                _logging.LogError(exception, "Scheduled Import finished, Send Result email");
            }
        }

        private void WriteImportLogResultRecord(ImportStatus importStatus, Guid scheduledTaskId)
        {
            _logging.LogDebug("Scheduled Import finished, update CMSImportScheduledTaskResult table");
            TimeSpan timeSpan = new TimeSpan(importStatus.Result.Duration.Ticks);
            CMSImportScheduledTaskResultDTO poco = new CMSImportScheduledTaskResultDTO
            {
                ScheduledTaskId = scheduledTaskId,
                Duration = Convert.ToInt32(timeSpan.TotalSeconds),
                Errors = importStatus.Result.Errors.Take(50).ToCsv(),
                Executed = DateTime.Now,
                Success = !importStatus.Result.Errors.Any()
            };
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            scope.Database.Save(poco);
        }

        private bool ScheduledTaskInProgress()
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            return scope.Database.ExecuteScalar<bool>("SELECT *  FROM CMSImportScheduledTaskDefinition WHERE InProgress = 1", Array.Empty<object>());
        }

        private void SetInProgressFlag(CMSImportScheduledTaskDefinitionDTO dto, bool inProgress)
        {
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            _logging.LogDebug($"Set in progress is {inProgress} for {dto.ScheduledTaskName}");
            dto.InProgress = inProgress;
            if (!inProgress)
            {
                dto.LastTimeExecuted = DateTime.Now;
            }
            scope.Database.Save(dto);
        }

        private void SetNextRuntime(CMSImportScheduledTaskDefinitionDTO dto)
        {
            _logging.LogDebug("Set Next Runtime");
            ScheduledTaskModel model = ConvertToModel(dto);
            dto.NextRunTime = CalculateNextRunTime(model);
            using Umbraco.Cms.Infrastructure.Scoping.IScope scope = _scope.CreateScope(IsolationLevel.Unspecified, RepositoryCacheMode.Unspecified, null, null, null, callContext: false, autoComplete: true);
            scope.Database.Save(dto);
        }
    }
}
