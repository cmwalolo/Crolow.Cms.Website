using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Components.Grid.Members
{
    [ViewComponent(Name = "gridLogin")]
    public class GridLoginViewComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridLoginViewComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(GridLogin card)
        {
            return View(card);
        }

    }
}
