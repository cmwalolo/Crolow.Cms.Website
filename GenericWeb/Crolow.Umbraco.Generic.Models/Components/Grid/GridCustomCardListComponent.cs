using AutoMapper;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridCustomComponent")]
    public class GridCustomCardListComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridCustomCardListComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(GridCustomComponent card)
        {
            var targetType = this.GetType().Assembly.GetTypes().Where(p => p.FullName == card.Component).FirstOrDefault();
            var parameters = new object[] { mapper, contextFactory, urlProvider };


            IGridCustomComponentBuilder customComponentBuilder =
                (IGridCustomComponentBuilder)Activator.CreateInstance(targetType, parameters);

            //            IGridCustomComponentBuilder customComponentBuilder = 
            //                (IGridCustomComponentBuilder) Activator.CreateInstance(card.Assembly, card.Component);
            var result = await customComponentBuilder.GetCustomObject(card);
            return View(card.Template, result);
        }
    }
}
