using Crolow.Cms.Core.Context;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Crolow.Cms.Core.Startup.Mvc
{
    public class ComponentViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //return viewLocations;
            var newUrls = new List<string>
            {
                    "~/Views/Themes/" + SiteContext.CurrentSiteSettingsModel(context.ActionContext.HttpContext).Theme + "/{0}.cshtml",
                    "~/Views/Themes/Default/{0}.cshtml",
                    "~/Views/Themes/" + SiteContext.CurrentSiteSettingsModel(context.ActionContext.HttpContext).Theme + "/{1}/{0}.cshtml",
                    "~/Views/Themes/Default/{1}/{0}.cshtml",
            };
            // Todo : Didn't found a way to check if those view exists
            // So we append the original viewlocations to the new ones.
            newUrls.AddRange(viewLocations);
            return newUrls.ToArray();
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["Theme"] = SiteContext.CurrentSiteSettingsModel(context.ActionContext.HttpContext)?.Theme ?? "";
        }
    }
}
