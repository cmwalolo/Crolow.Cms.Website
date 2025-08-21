using AutoMapper;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid.Members
{
    [ViewComponent(Name = "gridRegistration")]
    public class GridRegisterViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridRegisterViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(GridRegistration card)
        {
            return View(card);
        }

    }
}
