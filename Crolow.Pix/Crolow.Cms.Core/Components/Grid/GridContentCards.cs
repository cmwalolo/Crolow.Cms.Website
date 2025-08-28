using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridContentCards")]
    public class GridContentCardsViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IViewComponentHelper viewComponentHelper;

        public GridContentCardsViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, IViewComponentHelper viewComponentHelper)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.viewComponentHelper = viewComponentHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, GridContentCards item)
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

        private async Task<CardsModel?> GetItemsAsync(GridContentCards card)
        {
            var item = mapper.Map<CardsModel>(card);
            item.PropagateTemplate = true;
            item.Items = new List<CardItemModel>();
            foreach (var cardItem in card.Cards)
            {
                if (cardItem is ICardItemModelBuilder)
                {

                    var model = ((ICardItemModelBuilder)cardItem).GetCardItemModel();
                    if (card.PropagateTemplate)
                    {
                        model.ParentTemplate = card.Template;
                    }
                    item.Items.Add(model);

                }
            }

            return item;
        }
    }
}
