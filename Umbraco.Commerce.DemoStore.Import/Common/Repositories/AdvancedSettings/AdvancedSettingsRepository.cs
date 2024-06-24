using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.AdvancedSettings
{
    public class AdvancedSettingsRepository : IAdvancedSettingsRepository, IRepository<AdvancedSettingProvider, string>
    {
        private CMSImportAdvanceSettingsProviderCollection _advanceSettingsProviderCollection;

        public AdvancedSettingsRepository(CMSImportAdvanceSettingsProviderCollection advanceSettingsProviderCollection)
        {
            _advanceSettingsProviderCollection = advanceSettingsProviderCollection;
        }

        public AdvancedSettingProvider GetByPropertyEditorAlias(string alias)
        {
            return GetAll().FirstOrDefault((AdvancedSettingProvider p) => p.SupportedPropertyEditorAliasses.Contains(alias));
        }

        public AdvancedSettingProvider Single(string alias)
        {
            return GetAll().FirstOrDefault((AdvancedSettingProvider p) => p.Alias.Equals(alias, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<AdvancedSettingProvider> GetAll()
        {
            return _advanceSettingsProviderCollection.Where((AdvancedSettingProvider p) => p.IsSupported).ToList();
        }
    }

}
