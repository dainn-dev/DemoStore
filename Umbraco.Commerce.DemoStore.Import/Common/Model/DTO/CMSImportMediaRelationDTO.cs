using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model.DTO
{
    [TableName("CMSImportMediaRelation")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class CMSImportMediaRelationDTO
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("UmbracoMediaId")]
        public Guid UmbracoMediaId { get; set; }

        [Column("SourceUrl")]
        public string SourceUrl { get; set; }

        [Column("ByteSize")]
        public long ByteSize { get; set; }
    }
}
