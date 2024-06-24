using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Security;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PasswordHelper(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public string GeneratePassword()
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            return serviceScope.ServiceProvider.GetService<IMemberManager>().GeneratePassword();
        }

        public void SavePassword(IMember member, string password)
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            IMemberManager? service = serviceScope.ServiceProvider.GetService<IMemberManager>();
            MemberIdentityUser result = service.FindByNameAsync(member.Username).GetAwaiter().GetResult();
            service.ChangePasswordWithResetAsync(token: service.GeneratePasswordResetTokenAsync(result).GetAwaiter().GetResult(), userId: result.Id.ToString(), newPassword: password);
        }
    }
}
