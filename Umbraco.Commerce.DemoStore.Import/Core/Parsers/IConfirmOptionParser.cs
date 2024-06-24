namespace Umbraco.Commerce.DemoStore.Import.Core.Parsers
{
    public interface IConfirmOptionParser
    {
        string ParseConfirmOption(string propertyEditorAlias, object value);
    }
}
