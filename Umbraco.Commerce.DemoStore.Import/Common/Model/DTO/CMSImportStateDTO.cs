using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model.DTO
{
    [TableName("CMSImportState")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public class CMSImportStateDTO
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("UniqueIdentifier")]
        public Guid UniqueIdentifier { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Parent")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public Guid Parent { get; set; }

        [Column("ImportState")]
        [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
        public string ImportState { get; set; }

        [Column("ImportProvider")]
        public string ImportProvider { get; set; }
    }
}
