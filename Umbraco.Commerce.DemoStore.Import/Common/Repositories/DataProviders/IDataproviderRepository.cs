using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.DataProviders
{
    public interface IDataproviderRepository
    {
        IEnumerable<DataProvider> GetAll();

        IEnumerable<DataProvider> GetAllForContentType(string contentType);

        IProviderOptions GetProviderOptions(string alias);

        DataProvider Single(string alias);
    }
}
