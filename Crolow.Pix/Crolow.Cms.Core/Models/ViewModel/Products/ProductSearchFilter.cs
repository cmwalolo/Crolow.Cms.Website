using Crolow.Cms.Core.Models.ViewModel.Search;
using Microsoft.AspNetCore.Mvc;

namespace Crolow.Cms.Core.Models.ViewModel.Products
{

    public class ProductSearchFilter : BaseSearchFilter
    {
        [ModelBinder(BinderType = typeof(CsvArrayBinder))]
        public string[] Categories { get; set; }
        [ModelBinder(BinderType = typeof(CsvArrayBinder))]
        public string[] Tags { get; set; }
    }

    public class ProductSearchFilterOptions : BaseSearchFilterOptions
    {
        public List<KeyValuePair<string, string>> Categories { get; set; } = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> Tags { get; set; } = new List<KeyValuePair<string, string>>();
    }

}