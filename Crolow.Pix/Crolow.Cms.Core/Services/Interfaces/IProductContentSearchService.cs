using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Products;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Cms.Core.Services.Interfaces
{
    public interface IProductContentSearchService
    {
        void ExtractFilterOptions(IPublishedContent root, ProductSearchResultModel model);
        IEnumerable<ProductPage> Search(IPublishedContent root, ProductSearchResultModel model, out int totalItems);
    }
}