using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model.DTO
{
    [TableName("CMSImportScheduledTaskResult")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class CMSImportScheduledTaskResultDTO
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("ScheduledTaskId")]
        public Guid ScheduledTaskId { get; set; }

        [Column("Success")]
        public bool Success { get; set; }

        [Column("Executed")]
        public DateTime Executed { get; set; }

        [Column("Duration")]
        public int Duration { get; set; }

        [Column("Errors")]
        [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
        public string Errors { get; set; }
    }

}
