namespace Umbraco.Commerce.DemoStore.Import.Extentions.UI
{
    public interface IFieldValueConverter
    {
        object ToUIValue(object o);

        object FromUIValue(object o);
    }
}
