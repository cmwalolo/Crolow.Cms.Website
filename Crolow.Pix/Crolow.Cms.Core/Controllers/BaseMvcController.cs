using Crolow.Cms.Core.Context;
using Crolow.Core.Models.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Crolow.Core.Controllers.Pages
{
    public class BaseMvcController : RenderController
    {
        protected SiteSettingsModel Settings { get; set; }


        public BaseMvcController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            SiteContext.Current(context.HttpContext,
                                this.UmbracoContext.PublishedRequest?.PublishedContent);

            return base.OnActionExecutionAsync(context, next);
        }

        public SiteSettingsModel GetSettings(IPublishedContent root)
        {
            return SiteContext.Current(base.HttpContext)?.SettingsModel;
        }
    }

}
