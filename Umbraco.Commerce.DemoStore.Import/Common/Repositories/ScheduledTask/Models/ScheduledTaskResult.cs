namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ScheduledTask.Models
{
    public class ScheduledTaskResult
    {
        public bool Success { get; set; }

        public DateTime Executed { get; set; }

        public int Duration { get; set; }

        public string Errors { get; set; }
    }
}
