using Crolow.Cms.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Crolow.Core.Controllers.Pages
{
    [NonViewComponent]
    public abstract class CachedViewComponent<TModel> : BaseViewComponent where TModel : ICachableComponentModel
    {
        protected readonly AppCaches caches;

        protected CachedViewComponent(AppCaches caches)
        {
            this.caches = caches;
        }

        /// <summary>
        /// Retrieves a cached item or builds it using the provided factory function.
        /// </summary>
        protected TModel GetOrAddCache(IPublishedContent content, Func<TModel> factory, params string[] keyParts)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            // Include content Id and version in key
            var key = BuildCacheKey(content, keyParts);

            // Try get from RuntimeCache
            var cachedItem = caches.RuntimeCache.GetCacheItem<TModel>(key);
            if (cachedItem != null)
                return cachedItem;

            // Build item and insert into cache
            cachedItem = factory();
            caches.RuntimeCache.InsertCacheItem(key, () => cachedItem, TimeSpan.FromMinutes(30));
            return cachedItem;
        }

        /// <summary>
        /// Builds a cache key with site/language/contentId/version + optional additional key parts.
        /// </summary>
        protected string BuildCacheKey(IPublishedContent content, params string[] keyParts)
        {
            var lang = content.GetCultureFromDomains() ?? "";
            var site = content.AncestorOrSelf(1)?.Name;


            var allParts = new List<string> { site, lang, };
            if (keyParts != null && keyParts.Length > 0)
                allParts.AddRange(keyParts);

            return $"{typeof(TModel).Name}:{string.Join(":", allParts)}";
        }
    }
}
