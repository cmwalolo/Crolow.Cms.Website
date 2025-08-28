using Crolow.Cms.Core.Interfaces;
using Crolow.Cms.Core.Models.Enumerations;
using Umbraco.Cms.Core.Strings;

namespace Crolow.Cms.Core.Models.ViewModel.Cards
{
    public class ProductCardItemModel : CardItemModel
    {

    }

    public class CardItemModel : ICachableComponentModel
    {
        public int OriginId { get; set; }
        public long OriginVersion { get; set; }
        public string ParentTemplate { get; set; }
        public CardType CardType { get; set; }
        public CardSize CardTypeSize { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public IHtmlEncodedString Description { get; set; }
        public List<LinkModel> Links { get; set; }
        public string Image { get; set; }
        public bool DoNotCrop { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Price { get; set; }
    }
}
