using Microsoft.AspNetCore.Mvc.Localization;

namespace Crolow.Cms.Core.HtmlHelpers.Dictionary;

public interface IDictionaryLocalizer
{
    LocalizedHtmlString this[string key] { get; }
    LocalizedHtmlString this[string key, params object[] args] { get; }
}
