using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;
using Umbraco.Commerce.DemoStore.Import.Core.Composers.CollectionBuilders;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;
using Umbraco.Commerce.DemoStore.Import.Core.Providers.FieldProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Repositories.FieldProviders
{
    public class FieldProviderCollection : IFieldProviderCollection
    {
        private readonly ICachingHelper _cachingHelper;

        private readonly CMSImportFieldProviderCollection _fieldProviderCollection;

        private const string _cachingKey = "cmsimport.fieldproviders";

        public FieldProviderCollection(ICachingHelper cachingHelper, CMSImportFieldProviderCollection fieldProviderCollection)
        {
            _cachingHelper = cachingHelper;
            _fieldProviderCollection = fieldProviderCollection;
        }

        public IEnumerable<FieldProviderItem> GetAll()
        {
            List<FieldProviderItem> fromCache = _cachingHelper.GetFromCache<List<FieldProviderItem>>("cmsimport.fieldproviders");
            if (fromCache != null)
            {
                return fromCache;
            }
            fromCache = new List<FieldProviderItem>();
            foreach (IFieldProvider item in _fieldProviderCollection)
            {
                AddProviderInfo(item, fromCache);
            }
            _cachingHelper.AddToCache(fromCache, "cmsimport.fieldproviders");
            return fromCache;
        }

        private void AddProviderInfo(IFieldProvider item, List<FieldProviderItem> result)
        {
            foreach (FieldProviderAttribute attribute in AttributeHelper.GetAttributes<FieldProviderAttribute>(item))
            {
                result.Add(new FieldProviderItem
                {
                    PropertyEditorAlias = attribute.PropertyEditorAlias,
                    Prio = attribute.Priority,
                    FieldProvider = item
                });
            }
        }
    }
}
