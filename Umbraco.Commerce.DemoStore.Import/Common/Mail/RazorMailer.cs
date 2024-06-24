using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Commerce.DemoStore.Import.Core.Mail;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail.Model;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail;
using Umbraco.Commerce.DemoStore.Import.Core.Services;
using Umbraco.Cms.Core.Mail;

namespace Umbraco.Commerce.DemoStore.Import.Common.Mail
{

    public class RazorMailer : IRazorMailer
    {
        private readonly IRazorViewRenderService _razorViewRenderService;

        private readonly ILogger<RazorMailer> _logger;

        private readonly IEmailSender _emailSender;

        public RazorMailer(IRazorViewRenderService razorViewRenderService, ILogger<RazorMailer> logger, IEmailSender emailSender)
        {
            _razorViewRenderService = razorViewRenderService;
            _logger = logger;
            _emailSender = emailSender;
        }

        public void Send(BaseRazorMailModel model, EmailConfiguration config, string toAddress, string toName = null)
        {
            try
            {
                string result = _razorViewRenderService.RenderToStringAsync(config.Template, model).Result;
                string subject = config.Subject;
                EmailMessage message = new EmailMessage(config.FromAddress, toAddress, subject, result, isBodyHtml: true);
                _emailSender.SendAsync(message, "CMSImport");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Sending email");
            }
        }
    }
}
