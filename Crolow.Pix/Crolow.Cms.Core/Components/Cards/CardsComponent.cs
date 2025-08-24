using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Cards
{
    [ViewComponent(Name = "Cards")]
    public class CardsViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public CardsViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }
        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, CardsModel item)
        {
            return View(item.Template, item);
        }

    }
}
