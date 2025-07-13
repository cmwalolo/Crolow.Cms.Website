using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.AspNetCore.Mvc;
using Crolow.Core.Controllers.Pages;

namespace UmbracoProject.Controller
{
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