using System.Text.Json.Serialization;
using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders
{
    public abstract class AdvancedSettingProvider
    {
        public virtual bool IsSupported => true;

        [JsonIgnore]
        public IEnumerable<string> SupportedPropertyEditorAliasses { get; }

        [JsonIgnore]
        public bool CollapseByDefault { get; set; }

        public string Alias { get; }

        protected AdvancedSettingProvider()
        {
            AdvancedSettingsProviderAttribute advancedSettingsProviderAttribute = AttributeHelper.GetAttributes<AdvancedSettingsProviderAttribute>(this, attributeRequired: true).FirstOrDefault();
            if (advancedSettingsProviderAttribute != null)
            {
                Alias = advancedSettingsProviderAttribute.Alias;
                SupportedPropertyEditorAliasses = advancedSettingsProviderAttribute.SupportedPropertyEditorAliasses.ToList();
                CollapseByDefault = advancedSettingsProviderAttribute.CollapseByDefault;
            }
        }

        public abstract object Parse(object value, IContentBase importedItem, IProviderOptions options, ImportOptions importOptions);

        public abstract IEnumerable<string> Validate(IProviderOptions options);

        public abstract IProviderOptions GetAdvancedSettingProviderOptions(ImportPropertyInfo importPropertyInfo);
    }
}
