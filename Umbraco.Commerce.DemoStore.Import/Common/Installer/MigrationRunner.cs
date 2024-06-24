using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Umbraco.Commerce.DemoStore.Import.Common.Installer
{
    public class MigrationRunner : INotificationHandler<UmbracoApplicationStartingNotification>, INotificationHandler
    {
        private readonly IScopeProvider _scopeProvider;

        private readonly IMigrationBuilder _migrationBuilder;

        private readonly IKeyValueService _keyValueService;

        private readonly IMigrationPlanExecutor _migrationPlanExecutor;

        private readonly ILogger<MigrationRunner> _logger;

        public MigrationRunner(IMigrationPlanExecutor migrationPlanExecutor, 
            IScopeProvider scopeProvider, 
            IMigrationBuilder migrationBuilder, 
            IKeyValueService keyValueService, 
            ILogger<MigrationRunner> logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
            _migrationPlanExecutor = migrationPlanExecutor;
        }

        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            if (notification.RuntimeLevel >= RuntimeLevel.Run)
            {
                CMSImportMigrationPlan plan = new CMSImportMigrationPlan("CMSImport");
                _logger.LogDebug("CMSImport: Check install");
                new Upgrader(plan).Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
            }
        }
    }
}
