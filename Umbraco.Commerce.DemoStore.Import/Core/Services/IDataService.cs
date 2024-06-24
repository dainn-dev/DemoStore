using System.Data;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;

namespace Umbraco.Commerce.DemoStore.Import.Core.Services
{
    public interface IDataService
    {
        IDataReader GetData(IProviderOptions options);

        IEnumerable<string> GetGolumnsData(IProviderOptions options);

        DataProvider GetDataProvider(IProviderOptions options);
    }
}
