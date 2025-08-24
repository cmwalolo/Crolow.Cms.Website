using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoProject.Controller
{
    public class ProductPageController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : ContentPageController(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    [ResponseCache(CacheProfileName = "Default")]
    public class ContentPageController : BaseMvcController
    {

        public ContentPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }
        public override IActionResult Index()
        {
            return CurrentTemplate(CurrentPage);
        }
    }
}