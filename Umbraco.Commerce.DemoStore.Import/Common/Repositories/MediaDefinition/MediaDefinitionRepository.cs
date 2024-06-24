using Umbraco.Cms.Core.Services;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;
using Umbraco.Commerce.DemoStore.Import.Common.Repositories.MediaDefinition.Models;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.MediaDefinition
{
    public class MediaDefinitionRepository : IMediaDefinitionRepository
    {
        private ICachingHelper _cachingHelper;

        private IMediaTypeService _mediaTypeService;

        public MediaDefinitionRepository(ICachingHelper cachingHelper, IMediaTypeService mediaTypeService)
        {
            _cachingHelper = cachingHelper;
            _mediaTypeService = mediaTypeService;
        }

        public List<MediaDefinitionModel> GetAll()
        {
            List<MediaDefinitionModel> list = _cachingHelper.GetFromCache<List<MediaDefinitionModel>>("CMSImportMediaDefinitionModel");
            if (list == null)
            {
                list = (from a in _mediaTypeService.GetAll()
                        select new MediaDefinitionModel
                        {
                            Alias = a.Alias,
                            Name = a.Name
                        }).ToList();
                _cachingHelper.AddToCache(list, "CMSImportMediaDefinitionModel");
            }
            return list;
        }

        public MediaDefinitionModel Single(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
