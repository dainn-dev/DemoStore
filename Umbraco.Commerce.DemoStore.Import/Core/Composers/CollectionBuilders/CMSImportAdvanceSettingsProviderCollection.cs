using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public sealed class CMSImportAdvanceSettingsProviderCollection : BuilderCollectionBase<AdvancedSettingProvider>
    {
        public CMSImportAdvanceSettingsProviderCollection(Func<IEnumerable<AdvancedSettingProvider>> items)
            : base(items)
        {
        }
    }
}
