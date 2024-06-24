using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders
{
    public class CMSImportAdvancedSettingsProviderCollectionBuilder : LazyCollectionBuilderBase<CMSImportAdvancedSettingsProviderCollectionBuilder, CMSImportAdvanceSettingsProviderCollection, AdvancedSettingProvider>
    {
        protected override CMSImportAdvancedSettingsProviderCollectionBuilder This => this;

        protected override ServiceLifetime CollectionLifetime => ServiceLifetime.Transient;
    }
}
