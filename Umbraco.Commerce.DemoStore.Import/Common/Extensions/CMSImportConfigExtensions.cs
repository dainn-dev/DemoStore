using Microsoft.Extensions.Options;
using Umbraco.Commerce.DemoStore.Import.Common.Config;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class CMSImportConfigExtensions
    {
        public static CmsImportConfig GetValueWithDefaults(this IOptions<CmsImportConfig> options)
        {
            CmsImportConfig cmsImportConfig = options?.Value ?? new CmsImportConfig();
            if (cmsImportConfig.MediaConfig == null)
            {
                cmsImportConfig.MediaConfig = new MediaConfig
                {
                    AllowedDomains = new List<string>(),
                    AllowedFileExtensions = ".doc,.docx,.pdf,.ppt,.pptx,.rar,.xls,.xlsx,.zip".ToList(),
                    MediaImportLocation = "/wwwroot/",
                    MediaImportKeepFolderStructure = true,
                    MediaImportFileTypeAlias = "File",
                    MediaImportFolderTypeAlias = "Folder",
                    MediaImportImageTypeAlias = "Image"
                };
            }
            if (cmsImportConfig.IgnoredPropertyAliasses == null)
            {
                cmsImportConfig.IgnoredPropertyAliasses = "umbracoMemberFailedPasswordAttempts,umbracoMemberLastLockoutDate,umbracoMemberLastLogin,umbracoMemberLastPasswordChangeDate,umbracoMemberFailedPasswordAttempts,umbracoMemberIsApproved".ToList();
            }
            if (cmsImportConfig.LoginCredentialsMailConfig == null)
            {
                cmsImportConfig.LoginCredentialsMailConfig = new MailConfig
                {
                    FromAddress = "robot@cmsimport.com",
                    Subject = "Your account is ready",
                    ViewLocation = "~/App_Plugins/cmsimport/config/loginmail.cshtml"
                };
            }
            if (cmsImportConfig.ScheduledTaskMailConfig == null)
            {
                cmsImportConfig.ScheduledTaskMailConfig = new MailConfig
                {
                    FromAddress = "robot@cmsimport.com",
                    Subject = "Scheduled task executed",
                    ViewLocation = "~/App_Plugins/cmsimport/config/scheduledtaskmail.cshtml"
                };
            }
            return cmsImportConfig;
        }
    }

}
