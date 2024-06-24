using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model.DTO
{
    [TableName("CMSImportScheduledTaskDefinition")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class CMSImportScheduledTaskDefinitionDTO
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("StateId")]
        public Guid StateId { get; set; }

        [Column("ScheduledTaskId")]
        public Guid ScheduledTaskId { get; set; }

        [Column("IsInterval")]
        public bool IsInterval { get; set; }

        [Column("ScheduledTaskName")]
        public string ScheduledTaskName { get; set; }

        [Column("NotificationAddress")]
        public string NotificationAddress { get; set; }

        [Column("Interval")]
        public int Interval { get; set; }

        [Column("Days")]
        public string Days { get; set; }

        [Column("Hour")]
        public int Hour { get; set; }

        [Column("Minute")]
        public int Minute { get; set; }

        [Column("LastTimeExecuted")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? LastTimeExecuted { get; set; }

        [Column("NextRunTime")]
        public DateTime? NextRunTime { get; set; }

        [Column("InProgress")]
        public bool InProgress { get; set; }
    }
}
