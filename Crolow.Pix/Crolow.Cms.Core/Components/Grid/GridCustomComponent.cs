using AutoMapper;
using Crolow.Cms.Core.Interfaces;
using Crolow.Cms.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridCustomComponent")]
    public class GridCustomComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IViewComponentHelper viewComponentHelper;

        public GridCustomComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, IViewComponentHelper viewComponentHelper)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.viewComponentHelper = viewComponentHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, Models.Umbraco.GridCustomComponent item)
        {
            var customComponent = item.CustomComponentDefinition.FirstOrDefault() as CustomComponent; //?.FirstOrDefault<CustomComponent>();   

            var targetType = GetType().Assembly.GetTypes().Where(p => p.FullName == customComponent.Component).FirstOrDefault();
            var parameters = new object[] { mapper, contextFactory, urlProvider };


            ICustomComponentBuilder customComponentBuilder =
                (ICustomComponentBuilder)Activator.CreateInstance(targetType, parameters);

            var result = await customComponentBuilder.GetCustomObject(customComponent, item.CustomProperties);

            // If external component is set, we render it instead of the default template
            if (!string.IsNullOrEmpty(item.ExternalViewComponent))
            {
                ((IViewContextAware)viewComponentHelper).Contextualize(ViewContext);
                var rendered = await viewComponentHelper.InvokeAsync(
                    item.ExternalViewComponent,
                    new { content = content, item = item });

                return new HtmlContentViewComponentResult(rendered);
            }

            // Wrap the rendered IHtmlContent in a ContentViewComponentResult
            return View(!string.IsNullOrEmpty(item.Template) ? item.Template : customComponent.Template, result);
        }
    }
}
