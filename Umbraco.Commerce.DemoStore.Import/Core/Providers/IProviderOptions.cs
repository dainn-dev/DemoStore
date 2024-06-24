namespace Umbraco.Commerce.DemoStore.Import.Core.Providers
{
    public interface IProviderOptions
    {
        string Alias { get; }
        bool RenderField(string fieldAlias);
    }
}
