namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ConfirmOptionProviders
{
    public interface IConfirmOptionProvider
    {
        string PropertyEditorAlias { get; }

        object ParseConfirmOption(object value);
    }
}
