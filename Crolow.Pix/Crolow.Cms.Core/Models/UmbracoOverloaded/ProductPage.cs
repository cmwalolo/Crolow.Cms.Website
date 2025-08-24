using Crolow.Cms.Core.Models.ViewModel.Cards;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Models.Umbraco
{
    partial class ProductPage : ICardItemModelBuilder
    {
        public CardItemModel GetCardItemModel()
        {
            var product = this.Product as Product;

            return new CardItemModel
            {
                CardType = Enumerations.CardType.ProductCard,
                CardTypeSize = Enumerations.CardSize.Small,
                Title = product.Name,
                Summary = product.Summary,
                Description = product.Description,
                Image = product.Image.Url(),
                Price = product.Price.ToString(),
                Links = new List<LinkModel>
                {
                    new LinkModel
                    {
                        LinkType = "internal",
                        Target = "_self",
                        Title = "#Products.ViewProduct",
                        Url = this.Url()
                    }
                }
            };
        }
    }
}
