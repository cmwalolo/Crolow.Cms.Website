using Crolow.Cms.Core.Models.ViewModel.Cards;

namespace Crolow.Cms.Core.Models.ViewModel.Search
{
    internal interface ISearchResultModel
    {
        int ContentId { get; set; }
        BaseSearchFilter Filter { get; set; }
        string PageUrl { get; set; }
        CardsModel Result { get; set; }
        int TotalItems { get; set; }
    }
}