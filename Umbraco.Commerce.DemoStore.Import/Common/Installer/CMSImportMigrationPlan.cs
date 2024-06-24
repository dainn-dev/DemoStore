using Umbraco.Cms.Infrastructure.Migrations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Installer
{
    public class CMSImportMigrationPlan : MigrationPlan
    {
        public CMSImportMigrationPlan(string name)
            : base(name)
        {
            From(string.Empty).To<InstallV4Migration>("V9 DTO").To<SetPermissionsMigration>("V9");
        }
    }
}
