using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Crolow.Core.Controllers.Pages
{
    [NonViewComponent]
    public class BaseViewComponent
    {
        public static ViewViewComponentResult ExecuteView(ViewComponent component, string area, string view, object model)
        {
            return component.View(FindView(component, area, view), model);
        }

        public static string FindView(ViewComponent component, string area, string view)
        {
            string themedView = $"~/Views/Themes/{area}/{view}";
            component.ViewEngine.GetView("", themedView, false);

            return ViewExists(component, themedView).Success == false ? "" : themedView;
        }

        public static ViewEngineResult ViewExists(ViewComponent component, string viewName)
        {
            return component.ViewEngine.FindView(component.ViewContext, viewName, false);
        }

    }
}
