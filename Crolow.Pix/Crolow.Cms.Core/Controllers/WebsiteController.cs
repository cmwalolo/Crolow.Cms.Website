using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.AspNetCore.Mvc;
using Crolow.Core.Controllers.Pages;

namespace UmbracoProject.Controller
{
    [ResponseCache(NoStore = false, Duration = 3600, Location = ResponseCacheLocation.Client)]
    public class WebsiteController : BaseMvcController
    {

        public WebsiteController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            return this.Redirect("/fr");
        }

    }
}