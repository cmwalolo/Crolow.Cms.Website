using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Collections;
using System.Reflection;

namespace Crolow.Cms.Core.Extensions
{
    public static class FilterExtensions
    {
        public static string ToQueryString<T>(this T filter, bool includePage = true)
        {
            if (filter == null)
                return string.Empty;

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dict = props
                .Where(p => includePage || !string.Equals(p.Name, "Page", StringComparison.OrdinalIgnoreCase))
                .Select(p => new
                {
                    Name = p.Name,
                    Value = p.GetValue(filter)
                })
                .Where(x => x.Value != null)
                .ToDictionary(
                    x => x.Name,
                    x =>
                    {
                        if (x.Value is IEnumerable enumerable && !(x.Value is string))
                        {
                            var items = enumerable.Cast<object>()
                                .Select(o => o?.ToString())
                                .Where(s => !string.IsNullOrWhiteSpace(s));
                            return string.Join(",", items);
                        }
                        return x.Value.ToString();
                    });

            return QueryHelpers.AddQueryString("", dict).TrimStart('?');
        }

        public static T ParseFromQuery<T>(this IQueryCollection query) where T : new()
        {
            var filter = new T();
            var type = typeof(T);

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanWrite)
                    continue;

                // Check if the querystring contains this property name (case-insensitive)
                if (!query.TryGetValue(prop.Name, out StringValues values) || values.Count == 0)
                    continue;

                try
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(filter, values.ToString());
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        if (int.TryParse(values.ToString(), out var v)) prop.SetValue(filter, v);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(values.ToString(), out var v)) prop.SetValue(filter, v);
                    }
                    else if (prop.PropertyType == typeof(int[]))
                    {
                        var arr = values
                            .SelectMany(v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                            .Select(v => int.TryParse(v, out var i) ? i : (int?)null)
                            .Where(i => i.HasValue)
                            .Select(i => i.Value)
                            .ToArray();
                        prop.SetValue(filter, arr);
                    }
                    else if (prop.PropertyType == typeof(string[]))
                    {
                        var arr = values
                            .SelectMany(v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                            .Where(v => !string.IsNullOrWhiteSpace(v))
                            .ToArray();
                        prop.SetValue(filter, arr);
                    }
                    // You can extend here with DateTime, enums, etc.
                }
                catch
                {
                    // silently skip invalid parsing
                }
            }

            return filter;
        }
    }
}