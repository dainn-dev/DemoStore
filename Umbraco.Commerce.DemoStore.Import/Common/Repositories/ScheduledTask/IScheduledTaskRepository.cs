using Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask
{
    public interface IScheduledTaskRepository
    {
        IEnumerable<ScheduledTaskModel> GetAll();

        ScheduledTaskModel Single(Guid scheduledTaskId);

        void Delete(Guid scheduledTaskId);

        void Save(ScheduledTaskModel model);

        void TryExecuteNextImportTask();

        IEnumerable<ScheduledTaskResult> GetLog(Guid scheduledTaskId);

        void ClearLog(Guid scheduledTaskId);
    }
}
