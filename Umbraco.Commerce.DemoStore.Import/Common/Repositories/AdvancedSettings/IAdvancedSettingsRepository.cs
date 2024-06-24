using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.AdvancedSettings
{
    public interface IAdvancedSettingsRepository
    {
        AdvancedSettingProvider GetByPropertyEditorAlias(string alias);

        AdvancedSettingProvider Single(string alias);

        IEnumerable<AdvancedSettingProvider> GetAll();
    }
}
