using Microsoft.Extensions.Caching.Memory;
using Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class CachingHelper : ICachingHelper
    {
        private readonly IMemoryCache _memoryCache;

        private static object _lock = new object();

        private static List<string> _cacheIdentifiers = new List<string>();

        private const int CACHINGINMINUTES = 60;

        private List<string> CacheIds
        {
            get
            {
                return _cacheIdentifiers;
            }
            set
            {
                lock (_lock)
                {
                    _cacheIdentifiers = value;
                }
            }
        }

        public CachingHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void AddToCache(object itemToCache, string cacheIdentifier)
        {
            AddToCache(itemToCache, cacheIdentifier, 60);
        }

        public void AddToCache(object itemToCache, string cacheIdentifier, int timeOutInMinutes)
        {
            lock (_lock)
            {
                if (!_memoryCache.TryGetValue(cacheIdentifier, out object value) && value == null)
                {
                    AddCacheId(cacheIdentifier);
                    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(timeOutInMinutes));
                    _memoryCache.Set(cacheIdentifier, itemToCache, options);
                }
            }
        }

        public T GetFromCache<T>(string cacheIdentifier) where T : class
        {
            _memoryCache.TryGetValue<T>(cacheIdentifier, out T value);
            return value;
        }

        public void RemoveFromCache(string cacheIdentifier)
        {
            lock (_lock)
            {
                if (_memoryCache.TryGetValue(cacheIdentifier, out object _))
                {
                    _memoryCache.Remove(cacheIdentifier);
                }
            }
        }

        private void AddCacheId(string cacheId)
        {
            if (!CacheIds.Contains(cacheId))
            {
                CacheIds.Add(cacheId);
            }
        }

        public void ClearCache()
        {
            foreach (string cacheId in CacheIds)
            {
                RemoveFromCache(cacheId);
            }
            CacheIds = new List<string>();
        }
    }
}
