using System.Linq.Dynamic.Core;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Extensions
{

    public static class DynamicQueryExtensions
    {
        // 🔎 Search across multiple properties
        public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, string searchString, params string[] propertyNames)
        {
            if (string.IsNullOrWhiteSpace(searchString) || propertyNames == null || propertyNames.Length == 0)
                return query;

            // Build: "Name.Contains(@0) OR Title.Contains(@0) OR Summary.Contains(@0)"
            var conditions = propertyNames.Select(p => $"{p} != null && {p}.ToLower().Contains(@0)");
            var predicate = string.Join(" OR ", conditions);

            return query.Where(predicate, searchString.ToLowerInvariant());
        }

        // 🎯 Int array filter (example: categories, tags, etc.)
        public static IQueryable<T> ApplyIntArrayFilter<T>(this IQueryable<T> query, string propertyName, int[] values)
        {
            if (values == null || values.Length == 0)
                return query;

            // Build: "Categories.Any(@0.Contains(it))"
            // In Dynamic LINQ, @0 can be a collection
            return query.Where($"{propertyName}.Any(@0.Contains(it))", values);
        }

        public static IQueryable<T> ApplyStringArrayFilter<T>(this IQueryable<T> query, string propertyName, string[] values)
        {
            if (values == null || values.Length == 0)
                return query;

            // Build: "Categories.Any(@0.Contains(it))"
            // In Dynamic LINQ, @0 can be a collection
            return query.Where($"{propertyName}.Any(@0.Contains(it))", values);
        }

        // ↕️ Ordering
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string propertyName, bool descending)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return query;

            var ordering = descending ? $"{propertyName} descending" : propertyName;
            return query.OrderBy(ordering);
        }

        // 📑 Paging
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 20;

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}