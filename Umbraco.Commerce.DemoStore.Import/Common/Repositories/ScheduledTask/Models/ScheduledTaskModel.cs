namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask.Models
{
    public class ScheduledTaskModel
    {
        public Guid StateId { get; set; }

        public Guid ScheduledTaskId { get; set; }

        public int UserId { get; set; }

        public ScheduledTaskType ScheduledTaskType { get; set; }

        public string ScheduledTaskName { get; set; }

        public string NotificationAddress { get; set; }

        public int Interval { get; set; }

        public List<int> Days { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public DateTime? LastTimeExecuted { get; set; }

        public DateTime? NextRunTime { get; set; }

        public bool HasScheduledTaskResult { get; set; }
    }

    public enum ScheduledTaskType
    {
        Daily,
        Interval
    }
}
