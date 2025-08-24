using Crolow.Cms.Core.Models.ViewModel.Cards;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Models.Umbraco
{
    partial class ProductCategory : ICardItemModelBuilder
    {
        public CardItemModel GetCardItemModel()
        {
            return new CardItemModel
            {
                CardType = Enumerations.CardType.Default,
                CardTypeSize = Enumerations.CardSize.Small,
                Title = this.Name,
                Summary = this.Summary,
                Description = this.Description,
                Image = this.Image?.Url(),
                Links = new List<LinkModel>
                {
                    new LinkModel
                    {
                        LinkType = "internal",
                        Target = "_self",
                        Title = "#Products.ViewCategory",
                        Url = this.Url()
                    }
                }
            };
        }
    }
}
