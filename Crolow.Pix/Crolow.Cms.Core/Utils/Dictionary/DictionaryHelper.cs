using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace Crolow.Cms.Core.Utils.Dictionary;

public interface IDictionaryLocalizer
{
    LocalizedHtmlString this[string key] { get; }
    LocalizedHtmlString this[string key, params object[] args] { get; }
}

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

public static class HtmlHelperExtensions
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

public class DictionaryHelper
{
    private readonly ILocalizationService _localizationService;
    private readonly IVariationContextAccessor _variationContextAccessor;

    public DictionaryHelper(
        ILocalizationService localizationService,
        IVariationContextAccessor variationContextAccessor)
    {
        _localizationService = localizationService;
        _variationContextAccessor = variationContextAccessor;
    }

    public string GetDictionary(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return string.Empty;

        var culture = _variationContextAccessor.VariationContext?.Culture ?? "en-US";
        var lang = _localizationService.GetLanguageByIsoCode(culture);

        // Try get dictionary item
        var item = _localizationService.GetDictionaryItemByKey(key);

        if (item == null)
        {
            // Create it if missing
            item = CreateDictionaryItem(key, lang);
        }

        // Get translation or fallback
        var translation = item.Translations.FirstOrDefault(x => x.LanguageIsoCode == culture)?.Value;
        if (string.IsNullOrWhiteSpace(translation))
        {
            // Generate default label from last segment of key
            translation = GenerateLabelFromKey(key);
            _localizationService.AddOrUpdateDictionaryValue(item, lang, translation);
        }

        return translation;
    }

    private IDictionaryItem CreateDictionaryItem(string fullKey, ILanguage lang)
    {
        string[] parts = fullKey.Split('.');
        string parentKey = null;
        IDictionaryItem parent = null;

        foreach (var part in parts)
        {
            var currentKey = parentKey == null ? part : $"{parentKey}.{part}";
            var current = _localizationService.GetDictionaryItemByKey(currentKey);

            if (current == null)
            {
                var newItem = new DictionaryItem(currentKey)
                {
                    ParentId = parent?.Key ?? null
                };
                _localizationService.Save(newItem);
                current = newItem;
            }

            parent = current;
            parentKey = currentKey;
        }

        var label = GenerateLabelFromKey(fullKey);
        _localizationService.AddOrUpdateDictionaryValue(parent, lang, label);

        return parent;
    }


    private string GenerateLabelFromKey(string key)
    {
        var last = key.Split('.').Last();
        return Regex.Replace(last, "([a-z])([A-Z])", "$1 $2");
    }
}
