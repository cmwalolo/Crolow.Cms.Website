using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Components.Grid
{
    [ViewComponent(Name = "gridForm")]
    public class GridFormComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IUmbracoContextAccessor umbracoAccessor;

        public GridFormComponent(IUmbracoContextAccessor umbracoAccessor, IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.umbracoAccessor = umbracoAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, GridForm item)
        {
            var formModel = mapper.Map<GridFormModel>(item);
            formModel.Id = umbracoAccessor.GetRequiredUmbracoContext().PublishedRequest.PublishedContent.Id;
            return View(item.Template, formModel);
        }
    }
}
