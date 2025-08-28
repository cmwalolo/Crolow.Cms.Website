using Crolow.Cms.Core.Models.Enumerations;
using Crolow.Cms.Core.Models.ViewModel.Cards;

namespace Crolow.Cms.Core.Models.Umbraco
{
    internal interface ICardItemModelBuilder
    {
        CardItemModel GetCardItemModel(string parentTemplate = "", CardType type = CardType.Default, CardSize size = CardSize.Small);
    }
}