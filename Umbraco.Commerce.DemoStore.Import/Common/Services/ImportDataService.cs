using System.Data;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;
using Umbraco.Commerce.DemoStore.Import.Core.Services;

namespace Umbraco.Commerce.DemoStore.Import.Common.Services
{
    public class ImportDataService : IDataService
    {
        private readonly IDataproviderRepository _dataproviderRepository;

        public ImportDataService(IDataproviderRepository dataproviderRepository)
        {
            _dataproviderRepository = dataproviderRepository;
        }

        public IDataReader GetData(IProviderOptions options)
        {
            return _dataproviderRepository.Single(options.Alias).GetData(options);
        }

        public IEnumerable<string> GetGolumnsData(IProviderOptions options)
        {
            return _dataproviderRepository.Single(options.Alias).GetColumnNames(options);
        }

        public DataProvider GetDataProvider(IProviderOptions options)
        {
            return _dataproviderRepository.Single(options.Alias);
        }
    }
}
