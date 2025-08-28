using Crolow.Cms.Core.Extensions;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Crolow.Cms.Core.Models.ViewModel.Search;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Cms.Core.Components.PageElements
{
    [ViewComponent(Name = "pagination")]
    public class PaginationViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, BaseSearchResultModel item)
        {
            int windowSize = 5;
            var totalPages = (int)Math.Ceiling(item.TotalItems / (double)item.Filter.PageSize);
            var currentPage = Math.Max(1, item.Filter.Page);

            // Calculate page range
            var half = windowSize / 2;
            var startPage = Math.Max(1, currentPage - half);
            var endPage = Math.Min(totalPages, startPage + windowSize - 1);

            if (endPage - startPage + 1 < windowSize && startPage > 1)
                startPage = Math.Max(1, endPage - windowSize + 1);

            var model = new PaginationViewModel
            {
                BaseQuery = item.Filter.ToQueryString(false),
                CurrentPage = currentPage,
                TotalPages = totalPages,
                StartPage = startPage,
                EndPage = endPage
            };

            return View(model);
        }
    }
}