using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

public static class ProductContentExtensions
{
    public static string GetPageName(this IPublishedContent content)
    {
        string[] fallbackProperties = new string[] { "pageTitle", "Name" };
        return GetFallbackValue(content, fallbackProperties);
    }

    public static string GetMenuName(this IPublishedContent content)
    {
        string[] fallbackProperties = new string[] { "menuDisplayName", "pageTitle", "title", "Name" };
        return GetFallbackValue(content, fallbackProperties);
    }


    private static string GetFallbackValue(this IPublishedContent content, string[] fallbackProperties)
    {

        if (content == null || fallbackProperties == null || fallbackProperties.Length == 0)
            return string.Empty;

        foreach (var propName in fallbackProperties)
        {
            object value = null;

            // Handle special cases
            if (propName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                value = content.Name;
            }
            else if (propName.Equals("Url", StringComparison.OrdinalIgnoreCase))
            {
                value = content.Url();
            }
            else
            {
                value = content.Value<string>(propName);
            }

            if (value is string str && !string.IsNullOrWhiteSpace(str))
                return str;
        }

        return string.Empty;
    }
}
