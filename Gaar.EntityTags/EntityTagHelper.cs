using System;
using System.Runtime.Caching;

namespace Gaar.EntityTag
{
    public static class EntityTagHelper
    {
        private const string EtagCacheKey = "entityTagCacheKey";

        public static void UpdateEtagValueInCache(string spesificEtagCacheKey = null)
        {
            var cacheKey = !string.IsNullOrWhiteSpace(spesificEtagCacheKey) ? spesificEtagCacheKey : EtagCacheKey;
            var etagValue = DateTime.UtcNow.Ticks.ToString() + ":0";
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddYears(1);
            cache.Set(cacheKey, etagValue, policy);
        }

        public static string GetEtagValueInCache(string spesificEtagCacheKey = null)
        {
            var cacheKey = !string.IsNullOrWhiteSpace(spesificEtagCacheKey) ? spesificEtagCacheKey : EtagCacheKey;
            ObjectCache cache = MemoryCache.Default;
            string etagValue = (string)cache.Get(cacheKey);

            if (string.IsNullOrWhiteSpace(etagValue))
            {
                UpdateEtagValueInCache();
                return GetEtagValueInCache();
            }

            return etagValue;
        }
    }
}