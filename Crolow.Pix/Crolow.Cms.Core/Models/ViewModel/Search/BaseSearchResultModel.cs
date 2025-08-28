using Crolow.Cms.Core.Models.ViewModel.Cards;

namespace Crolow.Cms.Core.Models.ViewModel.Search
{
    public class BaseSearchResultModel : ISearchResultModel
    {
        public int ContentId { get; set; }
        public CardsModel Result { get; set; } = new CardsModel();
        public int TotalItems { get; set; }
        public BaseSearchFilter Filter { get; set; }
        public BaseSearchFilterOptions FilterOptions { get; set; }
        public string PageUrl { get; set; }
    }
}