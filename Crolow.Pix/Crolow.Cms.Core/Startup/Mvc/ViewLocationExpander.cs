using Crolow.Cms.Core.Context;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Crolow.Cms.Core.Startup.Mvc
{
    public class ComponentViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var ctxt = SiteContext.Current(context.ActionContext.HttpContext)?.SettingsModel;

            var newUrls = new List<string>();

            if (ctxt != null)
            {
                newUrls = new List<string>
                {
                        "~/Views/Themes/" + ctxt.Theme + "/{0}.cshtml",
                        "~/Views/Themes/Default/{0}.cshtml",
                        "~/Views/Themes/" + ctxt.Theme + "/{1}/{0}.cshtml",
                        "~/Views/Themes/Default/{1}/{0}.cshtml",
                };
            }
            else
            {
                newUrls = new List<string>
                {
                        "~/Views/Themes/Default/{0}.cshtml",
                        "~/Views/Themes/Default/{1}/{0}.cshtml",
                };
            }

            // Todo : Didn't found a way to check if those view exists
            // So we append the original viewlocations to the new ones.
            newUrls.AddRange(viewLocations);
            return newUrls.ToArray();
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["Theme"] = SiteContext.Current(context.ActionContext.HttpContext)?.SettingsModel?.Theme ?? "";
        }
    }
}
