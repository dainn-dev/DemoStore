using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Model.DTO
{
    [TableName("CMSImportRelation")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public class CMSImportRelationDTO
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("UmbracoID")]
        public Guid UmbracoID { get; set; }

        [Column("StateId")]
        public Guid StateId { get; set; }

        [Column("ParentStateId")]
        public Guid ParentStateId { get; set; }

        [Column("DefinitionAlias")]
        [Length(250)]
        public string DefinitionAlias { get; set; }

        [Column("DatasourceKey")]
        [Length(250)]
        public string DatasourceKey { get; set; }

        [Column("ImportProvider")]
        [Length(250)]
        public string ImportProvider { get; set; }

        [Column("Updated")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime Updated { get; set; }
    }
}
