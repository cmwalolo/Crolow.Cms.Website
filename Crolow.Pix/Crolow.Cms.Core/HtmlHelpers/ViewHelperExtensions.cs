using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;

namespace Crolow.Cms.Core.HtmlHelpers
{
    public static partial class HelperExtensions
    {
        /// <summary>
        /// Checks if a ViewComponent with the given name exists.
        /// </summary>
        public static bool ComponentExists(this IHtmlHelper html, string componentName)
        {
            var selector = html.ViewContext.HttpContext.RequestServices
                .GetService<IViewComponentSelector>();

            if (selector == null)
                return false;

            return selector.SelectComponent(componentName) != null;
        }


    }
}
