using Microsoft.Extensions.Logging;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers;
using Umbraco.Commerce.DemoStore.Import.Common.Model.DTO;

namespace Umbraco.Commerce.DemoStore.Import.Common.Installer
{
    public class InstallV4Migration : MigrationBase
    {
        public InstallV4Migration(IMigrationContext context, ILogger<InstallV4Migration> logger)
            : base(context)
        {
        }

        protected override void Migrate()
        {
            if (!TableExists(DTOHelper.GetTableName(typeof(CMSImportMediaRelationDTO))))
            {
                base.Create.Table<CMSImportMediaRelationDTO>().Do();
            }
            if (!TableExists(DTOHelper.GetTableName(typeof(CMSImportRelationDTO))))
            {
                base.Create.Table<CMSImportRelationDTO>().Do();
            }
            if (!TableExists(DTOHelper.GetTableName(typeof(CMSImportStateDTO))))
            {
                base.Create.Table<CMSImportStateDTO>().Do();
            }
            if (!TableExists(DTOHelper.GetTableName(typeof(CMSImportScheduledTaskDefinitionDTO))))
            {
                base.Create.Table<CMSImportScheduledTaskDefinitionDTO>().Do();
            }
            if (!TableExists(DTOHelper.GetTableName(typeof(CMSImportScheduledTaskResultDTO))))
            {
                base.Create.Table<CMSImportScheduledTaskResultDTO>().Do();
            }
        }
    }
}
