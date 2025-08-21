using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoProject.Controller
{
    [ResponseCache(CacheProfileName = "Default")]
    public class HomePageController : BaseMvcController
    {

        public HomePageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            return CurrentTemplate(CurrentPage);
        }

        public IActionResult HomePage()
        {
            return CurrentTemplate(CurrentPage);
        }
    }
}