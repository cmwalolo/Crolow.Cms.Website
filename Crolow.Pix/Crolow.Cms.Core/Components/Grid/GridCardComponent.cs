using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridCard")]
    public class GridCardViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridCardViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, GridCard item)
        {
            if (item != null)
            {
                using (var contextReference = contextFactory.EnsureUmbracoContext())
                {
                    var card = await GetItemsAsync(item);
                    return View("Default", (CardItemModel?)card);
                }
            }

            return View();
        }
        private async Task<CardItemModel?> GetItemsAsync(GridCard card)
        {
            var gridCardItem = mapper.Map<CardItemModel>(card);
            return gridCardItem;
        }
    }
}
