using Umbraco.Cms.Core.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition
{
    public interface IPasswordHelper
    {
        string GeneratePassword();

        void SavePassword(IMember member, string password);
    }
}
