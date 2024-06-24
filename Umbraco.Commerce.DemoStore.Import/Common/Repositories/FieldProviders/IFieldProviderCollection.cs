namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders
{
    public interface IFieldProviderCollection
    {
        IEnumerable<FieldProviderItem> GetAll();
    }
}
