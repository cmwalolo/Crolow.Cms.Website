using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridCards")]
    public class GridCardsViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridCardsViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, GridCards item)
        {
            if (item != null)
            {
                using (var contextReference = contextFactory.EnsureUmbracoContext())
                {
                    var cards = await GetItemsAsync(item);
                    return View((CardsModel?)cards);
                }
            }

            return View();
        }

        private async Task<CardsModel?> GetItemsAsync(GridCards card)
        {
            var item = mapper.Map<CardsModel>(card);
            return item;
        }
    }
}
