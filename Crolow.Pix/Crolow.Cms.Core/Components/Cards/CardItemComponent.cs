using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "CardItem")]
    public class CardItemViewComponent : ViewComponent // CachedViewComponent<CardItemModel>
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected readonly AppCaches caches;

        public CardItemViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, AppCaches caches) //: base(caches)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, CardItemModel item)
        {
            string template = $"{item.CardType}-{item.CardTypeSize}";
            if (!string.IsNullOrEmpty(item.ParentTemplate))
            {
                template = $"{item.ParentTemplate}/{template}";
            }
            return View(template, item);
        }
    }
}
