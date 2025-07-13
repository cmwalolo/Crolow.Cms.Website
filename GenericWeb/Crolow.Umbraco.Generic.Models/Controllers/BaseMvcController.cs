using Crolow.Cms.Core.Context;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Crolow.Core.Models.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Crolow.Core.Controllers.Pages
{
    public class BaseMvcController : RenderController
    {
        protected SiteSettingsModel Settings { get; set; }

        protected static object _lock = new object();
        protected static Dictionary<string, SiteSettingsModel> settingsDico = new Dictionary<string, SiteSettingsModel>();

        public BaseMvcController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            SiteContext.CurrentSiteSettingsModel(context.HttpContext,
                        GetSettings(this.UmbracoContext.Content.GetAtRoot().FirstOrDefault()));
            return base.OnActionExecutionAsync(context, next);
        }

        public static SiteSettingsModel GetSettings(IPublishedContent root)
        {
            if (settingsDico.ContainsKey(root.Name))
            {
                return settingsDico[root.Name];
            }

            lock (_lock)
            {
                SiteSettingsModel settings = new SiteSettingsModel();
                var item = root.Children<DataFolder>().FirstOrDefault()?.Children<SettingsFolder>()?.FirstOrDefault()?.Children<LayoutSettings>()?.FirstOrDefault();
                settings.Theme = item?.Theme ?? "crolow";

                if (!settingsDico.ContainsKey(root.Name))
                {
                    settingsDico.Add(root.Name, settings);
                }
                return settings;
            }
        }
    }

}
