using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Components.GlobalPageElements
{
    [ViewComponent(Name = "Seo")]
    public class SeoComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IScopeProvider scopeProvider;


        public SeoComponent(IScopeProvider scopeProvider, IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.scopeProvider = scopeProvider;

        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, object item = null)
        {
            //var newItem = mapper.Map<SeoModel>(content);
            //newItem.OGImage = GetMediaUrl(content, "OGImage", UrlMode.Absolute);
            //newItem.TwitterImage = GetMediaUrl(content, "TwitterImage", UrlMode.Absolute);
            //ForMember(p => p.TwitterImage, o => o.MapFrom(p => GetMediaUrl(p, "TwitterImage", UrlMode.Absolute)));

            return View(content);
        }

        private string GetMediaUrl(IPublishedContent content, string property, UrlMode urlMode)
        {
            var media = content.Value<MediaWithCrops>(property);
            if (media != null)
            {
                return media.Url(null, urlMode);
            }
            return null;
        }


    }
}
