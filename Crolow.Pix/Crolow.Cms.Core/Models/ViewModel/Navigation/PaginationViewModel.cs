namespace Crolow.Cms.Core.Models.ViewModel.Navigation
{
    public class PaginationViewModel
    {
        public string BaseQuery { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}