using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.UmbracoContext;
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

        public GridFormComponent(IUmbracoContextAccessor umbracoAccessor,IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.umbracoAccessor = umbracoAccessor; 
        }

        public async Task<IViewComponentResult> InvokeAsync(GridForm form)
        {
            var formModel = mapper.Map<GridFormModel>(form);
            formModel.Id = umbracoAccessor.GetRequiredUmbracoContext().PublishedRequest.PublishedContent.Id;
            return View(form.Template, formModel);
        }
    }
}
