using AutoMapper;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

public class StringPropertyFallBackResolver<T> : IValueResolver<IPublishedContent, T, string>
{
    private readonly string[] _properties;

    public StringPropertyFallBackResolver(string[] properties)
    {
        _properties = properties;
    }

    public string Resolve(IPublishedContent source, T destination, string destMember, ResolutionContext context)
    {
        foreach (var propName in _properties)
        {
            object value = null;

            // Handle special cases
            if (propName.Equals("Name", StringComparison.OrdinalIgnoreCase))
                value = source.Name;
            else if (propName.Equals("Url", StringComparison.OrdinalIgnoreCase))
                value = source.Url();
            else
            {
                value = source.Value<string>(propName);
            }

            if (value is string str && !string.IsNullOrWhiteSpace(str))
                return str;
        }

        return string.Empty;
    }
}
