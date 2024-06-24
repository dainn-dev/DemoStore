using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Mail.Model;

namespace Umbraco.Commerce.DemoStore.Import.Core.Mail
{
    public interface IRazorMailer
    {
        void Send(BaseRazorMailModel model, EmailConfiguration config, string toAddress, string toName = null);
    }
}
