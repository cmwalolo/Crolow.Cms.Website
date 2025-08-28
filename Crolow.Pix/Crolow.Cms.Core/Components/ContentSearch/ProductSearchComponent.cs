using AutoMapper;
using Crolow.Cms.Core.Extensions;
using Crolow.Cms.Core.Models.Enumerations;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Crolow.Cms.Core.Models.ViewModel.Products;
using Crolow.Cms.Core.Models.ViewModel.Search;
using Crolow.Cms.Core.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Cards
{
    [ViewComponent(Name = "ProductSearch")]
    public class ProductSearchViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IProductContentSearchService searchService;
        protected IViewComponentHelper viewComponentHelper;


        public ProductSearchViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, IProductContentSearchService searchService, IViewComponentHelper viewComponentHelper)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.searchService = searchService;
            this.viewComponentHelper = viewComponentHelper;
        }
        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, object item = null)
        {
            var productFilter = this.HttpContext.Request.Query.ParseFromQuery<ProductSearchFilter>();
            var model = new ProductSearchResultModel { Filter = productFilter };
            var result = searchService.Search(content, model, out int totalItems);
            searchService.ExtractFilterOptions(content, model);

            model = new ProductSearchResultModel
            {
                ContentId = content.Id,
                TotalItems = totalItems,
                ProductFilter = productFilter,
                Products = new CardsModel
                {
                    Template = "Listing",
                    Items = result.Select(p => p.GetCardItemModel("Listing", CardType.ProductCard, CardSize.Large) as CardItemModel).ToList()
                },
                PageUrl = urlProvider.GetUrl(content, UrlMode.Relative),
            };


            ((IViewContextAware)viewComponentHelper).Contextualize(ViewContext);

            var builder = new HtmlContentBuilder();


            var rendered = await viewComponentHelper.InvokeAsync(
                "Cards",
                new { content = content, item = model.Products }
            );
            builder.AppendHtml(rendered);

            rendered = await viewComponentHelper.InvokeAsync(
                    "pagination",
                    new { content = content, item = model as BaseSearchResultModel }
                );

            builder.AppendHtml(rendered);

            return new HtmlContentViewComponentResult(builder);
        }

    }
}
