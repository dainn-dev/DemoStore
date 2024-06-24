namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.AdvancedSettingProviders
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdvancedSettingsProviderAttribute : Attribute
    {
        public string Alias { get; set; }

        public string SupportedPropertyEditorAliasses { get; set; }

        public bool CollapseByDefault { get; set; }
    }
}
