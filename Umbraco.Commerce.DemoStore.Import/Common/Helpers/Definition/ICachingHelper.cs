namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers.Definition
{
    public interface ICachingHelper
    {
        void AddToCache(object itemToCache, string cacheIdentifier);

        void AddToCache(object itemToCache, string cacheIdentifier, int timeOutInMinutes);

        T GetFromCache<T>(string cacheIdentifier) where T : class;

        void RemoveFromCache(string cacheIdentifier);

        void ClearCache();
    }
}
