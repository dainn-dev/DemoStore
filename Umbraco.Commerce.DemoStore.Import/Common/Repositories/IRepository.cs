namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories
{
    public interface IRepository<T, TU>
    {
        T Single(TU key);
        IEnumerable<T> GetAll();
    }
}
