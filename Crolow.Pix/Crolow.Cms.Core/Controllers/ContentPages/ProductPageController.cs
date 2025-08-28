using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoProject.Controller;

namespace Crolow.Cms.Core.Controllers.ContentPages
{
    public class ProductPageController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : ContentPageController(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }
}