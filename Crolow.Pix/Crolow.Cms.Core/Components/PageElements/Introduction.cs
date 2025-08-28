using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;


namespace Crolow.Cms.Core.Components.PageElements
{
    [ViewComponent(Name = "introduction")]
    public class IntroductionViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public IntroductionViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, PageElementComponent item = null)
        {
            return View(content);
        }

    }
}
