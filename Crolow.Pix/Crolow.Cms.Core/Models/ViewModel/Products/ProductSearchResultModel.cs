using Crolow.Cms.Core.Models.ViewModel.Cards;
using Crolow.Cms.Core.Models.ViewModel.Search;

namespace Crolow.Cms.Core.Models.ViewModel.Products
{
    public class ProductSearchResultModel : BaseSearchResultModel
    {
        public CardsModel Products
        {
            get { return Result as CardsModel; }
            set { Result = value; }
        }

        public ProductSearchFilter ProductFilter
        {
            get { return base.Filter as ProductSearchFilter; }
            set { base.Filter = value; }
        }

        public ProductSearchFilterOptions ProductSearchFilterOptions
        {

            get { return base.FilterOptions as ProductSearchFilterOptions; }
            set { base.FilterOptions = value; }
        }
    }
}