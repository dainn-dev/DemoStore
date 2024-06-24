namespace Umbraco.Commerce.DemoStore.Import.Common.Config
{
    public class CmsImportConfig
    {
        public MailConfig LoginCredentialsMailConfig { get; set; }

        public MailConfig ScheduledTaskMailConfig { get; set; }

        public MediaConfig MediaConfig { get; set; }

        public List<string> IgnoredPropertyAliasses { get; set; }
    }
}
