using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.AspNetCore.Mvc;
using Crolow.Core.Controllers.Pages;
using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Media;

namespace UmbracoProject.Controller
{
    [ResponseCache(NoStore = false, Duration = 3600, Location = ResponseCacheLocation.Client)]
    public class MediaCollectionPageController : BaseMvcController
    {
        protected IMapper mapper;
        public MediaCollectionPageController(IMapper mapper, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            this.mapper = mapper;
        }

        public override IActionResult Index()
        {
            var item = mapper.Map<MediaCollectionPageModel>(CurrentPage);
            this.ViewData["Extra"] = item;

            return CurrentTemplate(CurrentPage);
        }

    }
}