using Microsoft.Extensions.Logging;
using System.Net;
using Umbraco.Cms.Core.Runtime;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.HostedServices;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;
using Microsoft.AspNetCore.Hosting;

namespace Umbraco.Commerce.DemoStore.Import.Common.Scheduler
{
    public class RunSchedulerComponent : RecurringHostedServiceBase
    {
        private readonly IRuntimeState _runtimeState;

        private readonly IServerRoleAccessor _serverRegistrar;

        private readonly IMainDom _mainDom;

        private readonly ILogger<RunSchedulerComponent> _logger;

        private readonly IIOHelper _ioHelper;

        public RunSchedulerComponent(IWebHostEnvironment hostingEnvironment, 
            IRuntimeState runtime, 
            ILogger<RunSchedulerComponent> logger, 
            IServerRoleAccessor serverRegistrar, 
            IMainDom mainDom, 
            IIOHelper ioHelper)
            : base(logger, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0))
        {
            _runtimeState = runtime;
            _logger = logger;
            _serverRegistrar = serverRegistrar;
            _mainDom = mainDom;
            _ioHelper = ioHelper;
        }

        public override Task PerformExecuteAsync(object state)
        {
            try
            {
                if (_runtimeState.Level != RuntimeLevel.Run)
                {
                    return Task.CompletedTask;
                }
                switch (_serverRegistrar.CurrentServerRole)
                {
                    case ServerRole.Subscriber:
                        _logger.LogDebug("Does not run on replica servers.");
                        return Task.CompletedTask;
                    case ServerRole.Unknown:
                        _logger.LogDebug("Does not run on servers with unknown role.");
                        return Task.CompletedTask;
                    default:
                        {
                            if (!_mainDom.IsMainDom)
                            {
                                _logger.LogDebug("Does not run if not MainDom.");
                                return Task.CompletedTask;
                            }
                            string text = _ioHelper.FormatPluginUrl("CmsImportTaskSchedulerApi/ExecuteScheduledTasks");
                            string text2 = _ioHelper.GetApplicationUrl() + text;
                            try
                            {
                                _ioHelper.ParseRequest(text2);
                            }
                            catch (WebException)
                            {
                            }
                            catch (Exception ex2)
                            {
                                _logger.LogError("CMSImport Scheduled task Handler " + text2 + " not configured, see manual", ex2);
                            }
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error running scheduled task");
            }
            return Task.CompletedTask;
        }
    }
}
