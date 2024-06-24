using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Installer
{
    public class SetPermissionsMigration : MigrationBase
    {
        private readonly ILogger<SetPermissionsMigration> _loggingProvider;

        private readonly IUserService _userService;

        public SetPermissionsMigration(IMigrationContext context, ILogger<SetPermissionsMigration> loggingProvider, IUserService userService)
            : base(context)
        {
            _loggingProvider = loggingProvider;
            _userService = userService;
        }

        protected override void Migrate()
        {
            GrantPermissionForAppAction("admin", "cmsimport");
        }

        private void GrantPermissionForAppAction(string groupname, string appName)
        {
            try
            {
                IUserGroup userGroupByAlias = _userService.GetUserGroupByAlias(groupname);
                if (!userGroupByAlias.AllowedSections.Contains(appName))
                {
                    userGroupByAlias.AddAllowedSection(appName);
                }
                _userService.Save(userGroupByAlias);
            }
            catch (Exception exception)
            {
                _loggingProvider.LogError(exception, "Error installing SetPermissions migration");
            }
        }
    }
}
