namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FieldProviderAttribute : Attribute
    {
        public string PropertyEditorAlias { get; set; }

        public int Priority { get; set; }

        public FieldProviderAttribute()
        {
            Priority = 100;
        }
    }
}
