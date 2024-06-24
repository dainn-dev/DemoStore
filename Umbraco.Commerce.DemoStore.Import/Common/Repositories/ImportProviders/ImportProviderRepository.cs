using Umbraco.Cms.Core.Services;
using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions.UmbracoExtensions;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.ImportProviders
{
    public class ImportProviderRepository : IRepository<ImportProvider, string>, IImportProviderRepository
    {
        private readonly ILocalizedTextService _textService;

        private readonly CMSImportImportProviderCollection _importImportProviderCollection;

        public ImportProviderRepository(ILocalizedTextService textService, CMSImportImportProviderCollection importProviderCollection)
        {
            _textService = textService;
            _importImportProviderCollection = importProviderCollection;
        }

        public ImportProvider Single(string alias)
        {
            ImportProvider importProvider = GetAll().FirstOrDefault((ImportProvider p) => p.Alias.Equals(alias));
            if (importProvider != null)
            {
                importProvider.Title = _textService.LocalizeString(importProvider.Title);
                importProvider.Intro = _textService.LocalizeString(importProvider.Intro);
            }
            return importProvider;
        }

        public IEnumerable<ImportProvider> GetAll()
        {
            return from p in _importImportProviderCollection
                   where p.IsSupported
                   orderby p.SortOrder
                   select p;
        }

        public IProviderOptions GetProviderOptions(string alias)
        {
            return GetAll().FirstOrDefault((ImportProvider p) => p.Alias.Equals(alias))?.GetImportProviderOptions();
        }
    }

}
