namespace Umbraco.Commerce.DemoStore.Import.Core.Models.Mail
{
    public class ScheduledTaskMail : BaseRazorMailModel
    {
        public string TaskName { get; set; }

        public int RecordCount { get; set; }

        public int RecordsAdded { get; set; }

        public int RecordsUpdated { get; set; }

        public int RecordsSkipped { get; set; }

        public int RecordsDeleted { get; set; }

        public int Errors { get; set; }

        public List<string> ErrorMessages { get; private set; }

        public ScheduledTaskMail()
        {
            ErrorMessages = new List<string>();
        }
    }
}
