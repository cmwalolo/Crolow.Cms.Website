using Crolow.Cms.Core.Extensions;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Products;
using Crolow.Cms.Core.Services.Interfaces;
using System.Linq.Dynamic.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Services.Implementations
{
    public class ProductContentSearchService : IProductContentSearchService
    {

        public void ExtractFilterOptions(IPublishedContent root, ProductSearchResultModel model)
        {
            model.FilterOptions = new ProductSearchFilterOptions();
            model.FilterOptions.OrderOptions.Add(new KeyValuePair<string, string>("Name", "Name"));

            var products = root.Descendants<ProductPage>().Select(p => p.Product as Product).ToList();
            var categories = products.SelectMany(p => p.Categories).Distinct();
            var tags = products.SelectMany(p => p.Tags).Distinct();

            model.ProductSearchFilterOptions.Categories.AddRange(categories.Select(p => new KeyValuePair<string, string>(p, p)));
            model.ProductSearchFilterOptions.Tags.AddRange(tags.Select(p => new KeyValuePair<string, string>(p, p)));
        }


        public IEnumerable<ProductPage> Search(IPublishedContent root, ProductSearchResultModel model, out int totalItems)
        {
            if (root == null)
            {
                totalItems = 0;
                return Enumerable.Empty<ProductPage>();
            }

            // Get all descendants as a list (materialize once)
            var allResults = root.Descendants<ProductPage>().AsQueryable()
                .ApplySearch(model.Filter.SearchString, "Name", "Product.Title", "Product.Summary", "Product.Description")
                .ApplyStringArrayFilter("Product.Categories", model.ProductFilter.Categories)
                .ApplyStringArrayFilter("Product.Tags", model.ProductFilter.Tags)
                .ApplyOrdering(model.Filter.Order, model.ProductFilter.IsDescending)
                .ToList(); // ⚡ executes here

            totalItems = allResults.Count;

            // Apply paging in-memory
            return allResults
                .Skip((model.ProductFilter.Page - 1) * model.ProductFilter.PageSize)
                .Take(model.ProductFilter.PageSize)
                .ToList();
        }
    }
}