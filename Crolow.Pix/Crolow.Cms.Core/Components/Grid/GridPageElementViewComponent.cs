using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridPageElementComponent")]
    public class GridPageElementViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IViewComponentHelper viewComponentHelper;

        public GridPageElementViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, IViewComponentInvokerFactory invokerFactory, IViewComponentSelector viewComponentSelector, IViewComponentHelper viewComponentHelper)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.viewComponentHelper = viewComponentHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, Models.Umbraco.GridPageElementComponent item)
        {
            ((IViewContextAware)viewComponentHelper).Contextualize(ViewContext);

            var customComponent = item.CustomComponentDefinition.FirstOrDefault() as PageElementComponent; //?.FirstOrDefault<CustomComponent>();   

            var rendered = await viewComponentHelper.InvokeAsync(
                customComponent.Component,
                new { content = content, item = customComponent });

            return new HtmlContentViewComponentResult(rendered);
        }
    }
}
