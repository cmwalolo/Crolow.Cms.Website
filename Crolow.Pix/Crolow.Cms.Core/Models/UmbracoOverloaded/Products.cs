using Crolow.Cms.Core.Models.Enumerations;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Models.Umbraco
{
    partial class Products : ICardItemModelBuilder
    {
        string cardImageSize = "height=600&rmode=max";
        public CardItemModel GetCardItemModel(string parentTemplate = "", CardType type = CardType.Default, CardSize size = CardSize.Small)
        {
            return new CardItemModel
            {
                ParentTemplate = parentTemplate,
                CardType = type,
                CardTypeSize = size,
                Title = this.GetPageName(),
                Summary = this.Summary,
                Description = this.Description,
                Image = $"{this.MenuDisplayImage?.Url()}?{cardImageSize}",
                Links = new List<LinkModel>
                {
                    new LinkModel
                    {
                        LinkType = "internal",
                        Target = "_self",
                        Title = "#Products.ViewAllProducts",
                        Url = this.Url()
                    }
                }
            };
        }
    }
}
