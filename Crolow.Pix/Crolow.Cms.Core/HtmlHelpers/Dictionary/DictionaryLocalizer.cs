using Microsoft.AspNetCore.Mvc.Localization;

namespace Crolow.Cms.Core.HtmlHelpers.Dictionary;

public class DictionaryLocalizer : IDictionaryLocalizer
{
    private readonly DictionaryHelper _helper;

    public DictionaryLocalizer(DictionaryHelper helper)
    {
        _helper = helper;
    }

    public LocalizedHtmlString this[string key]
    {
        get
        {
            var value = _helper.GetDictionary(key);
            return new LocalizedHtmlString(key, value ?? key, isResourceNotFound: value == null);
        }
    }

    public LocalizedHtmlString this[string key, params object[] args]
    {
        get
        {
            var value = _helper.GetDictionary(key) ?? key;
            var formatted = string.Format(value, args);
            return new LocalizedHtmlString(key, formatted, isResourceNotFound: value == null);
        }
    }
}
