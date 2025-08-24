using Crolow.Cms.Core.HtmlHelpers.Dictionary;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Crolow.Cms.Core.HtmlHelpers;

public static partial class HelperExtensions
{
    public static IDictionaryLocalizer Dictionary(this IHtmlHelper htmlHelper)
    {
        return htmlHelper.ViewContext.HttpContext.RequestServices
            .GetRequiredService<IDictionaryLocalizer>();
    }

    public static string Dictionary(this IHtmlHelper htmlHelper, string key)
    {
        var httpContext = htmlHelper.ViewContext.HttpContext;
        var helper = httpContext.RequestServices.GetService(serviceType: typeof(DictionaryHelper)) as DictionaryHelper;
        return helper?.GetDictionary(key) ?? key;
    }
}
