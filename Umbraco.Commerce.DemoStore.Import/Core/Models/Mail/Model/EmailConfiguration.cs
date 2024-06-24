namespace Umbraco.Commerce.DemoStore.Import.Core.Models.Mail.Model
{
    public class EmailConfiguration
    {
        public string Alias { get; set; }

        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public string Subject { get; set; }

        public string Template { get; set; }
    }
}
