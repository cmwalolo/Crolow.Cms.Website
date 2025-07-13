using System.Linq;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace Umbraco.Cms.Infrastructure.Services.Implement
{
    public class CustomNewsArticleService : ICustomNewsArticleService
    {
        private readonly IMediaService _mediaService;
        private readonly ILogger<CustomNewsArticleService> _logger;
        private readonly IUmbracoContextFactory _contextFactory;

        public CustomNewsArticleService(ILogger<CustomNewsArticleService> logger, IUmbracoContextFactory contextFactory, IMediaService mediaService)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _mediaService = mediaService;
        }

        public void DoSomethingWithNewsArticles()
        {
            using (var contextReference = _contextFactory.EnsureUmbracoContext())
            {
                IPublishedContentCache contentCache = contextReference.UmbracoContext.Content;
                IPublishedContent newsSection = contentCache.GetAtRoot().FirstOrDefault().Children.FirstOrDefault(f => f.ContentType.Alias == "newsSection");
                if (newsSection == null)
                {
                    _logger.LogDebug("News Section Not Found");
                }
            }
            // etc
        }
    }
}