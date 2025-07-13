using AutoMapper;
using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.ViewModel;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Components
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

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content)
        {
            var newItem = mapper.Map<SeoModel>(content);
            //newItem.OGImage = GetMediaUrl(content, "OGImage", UrlMode.Absolute);
            //newItem.TwitterImage = GetMediaUrl(content, "TwitterImage", UrlMode.Absolute);
            //ForMember(p => p.TwitterImage, o => o.MapFrom(p => GetMediaUrl(p, "TwitterImage", UrlMode.Absolute)));

            return View(newItem);
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
