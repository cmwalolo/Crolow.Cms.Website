namespace Crolow.Cms.Core.Models.ViewModel.Search
{
    public class BaseSearchFilter
    {
        public string Order { get; set; } = "Name";               // property name to order by
        public bool IsDescending { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string SearchString { get; set; }

    }

    public class BaseSearchFilterOptions
    {
        public List<KeyValuePair<string, string>> OrderOptions { get; set; } = new List<KeyValuePair<string, string>>();
    }
}