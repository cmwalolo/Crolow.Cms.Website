using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public GridCustomComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Umbraco.GridCustomComponent card)
        {
            var targetType = GetType().Assembly.GetTypes().Where(p => p.FullName == card.Component).FirstOrDefault();
            var parameters = new object[] { mapper, contextFactory, urlProvider };


            IGridCustomComponentBuilder customComponentBuilder =
                (IGridCustomComponentBuilder)Activator.CreateInstance(targetType, parameters);

            var result = await customComponentBuilder.GetCustomObject(card);
            return View(card.Template, result);
        }
    }
}
