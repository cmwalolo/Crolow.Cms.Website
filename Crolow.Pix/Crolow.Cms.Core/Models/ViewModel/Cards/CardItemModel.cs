using Crolow.Cms.Core.Models.Enumerations;
using Umbraco.Cms.Core.Strings;

namespace Crolow.Cms.Core.Models.ViewModel.Cards
{
    public class LinkModel
    {
        public string LinkType { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
    }
    public class CardItemModel
    {
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
