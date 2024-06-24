using Umbraco.Cms.Core.Services;
using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.DataProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.DataProviders
{
    public class DataproviderRepository : IRepository<DataProvider, string>, IDataproviderRepository
    {
        private readonly ILocalizedTextService _textService;

        private readonly CMSImportDataProviderCollection _dataProviderCollection;

        public DataproviderRepository(ILocalizedTextService textService, CMSImportDataProviderCollection dataProviderCollection)
        {
            _textService = textService;
            _dataProviderCollection = dataProviderCollection;
        }

        public DataProvider Single(string alias)
        {
            DataProvider dataProvider = GetAll().FirstOrDefault((DataProvider p) => p.Alias.Equals(alias));
            if (dataProvider != null)
            {
                dataProvider.Title = _textService.LocalizeString(dataProvider.Title);
                dataProvider.Intro = _textService.LocalizeString(dataProvider.Intro);
            }
            return dataProvider;
        }

        public IEnumerable<DataProvider> GetAllForContentType(string contentType)
        {
            return from d in GetAll()
                   where contentType == null || !d.AllowedContentTypes.Any() || d.AllowedContentTypes.Contains(contentType)
                   select d;
        }

        public IProviderOptions GetProviderOptions(string alias)
        {
            return GetAll().FirstOrDefault((DataProvider p) => p.Alias.Equals(alias))?.GetDataProviderOptions();
        }

        public IEnumerable<DataProvider> GetAll()
        {
            return _dataProviderCollection.Where((DataProvider d) => d.IsSupported);
        }
    }
}
