using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

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

        public async Task<IViewComponentResult> InvokeAsync(GridCard card)
        {
            if (card != null)
            {
                using (var contextReference = contextFactory.EnsureUmbracoContext())
                {
                    var item = await GetItemsAsync(card);
                    return View("Default", item);
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
