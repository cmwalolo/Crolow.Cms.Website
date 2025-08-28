using Crolow.Cms.Core.Models.Enumerations;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Models.Umbraco
{
    partial class ProductPage : ICardItemModelBuilder
    {
        public CardItemModel GetCardItemModel(string parentTemplate = "", CardType type = CardType.Default, CardSize size = CardSize.Small)
        {
            var product = this.Product as Product;

            return new ProductCardItemModel()
            {
                ParentTemplate = parentTemplate,
                CardType = CardType.ProductCard,
                CardTypeSize = size,
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
