using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Crolow.Cms.Core.Extensions
{
    public static class CoreExtensions
    {
        /// <summary>
        /// Test if a view exists
        /// </summary>
        public static bool ViewExists(this ControllerBase controller, string name)
        {
            var services = controller.HttpContext.RequestServices;
            var viewEngine = services.GetRequiredService<ICompositeViewEngine>();
            var result = viewEngine.GetView(null, name, true);
            if (!result.Success)
                result = viewEngine.FindView(controller.ControllerContext, name, true);
            return result.Success;
        }

        public static bool ViewExists(this HttpContext context, string name)
        {
            var services = context.RequestServices;
            var viewEngine = services.GetRequiredService<ICompositeViewEngine>();
            var result = viewEngine.GetView(null, name, true);
            return result.Success;
        }


    }
}
