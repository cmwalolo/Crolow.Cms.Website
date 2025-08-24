using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Strings;

namespace Crolow.Cms.Core.Models.ViewModel.Cards
{
    public class CardsModel
    {
        public string Template { get; set; }

        public List<CardItemModel> Items { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IHtmlEncodedString Body { get; set; }
        public List<Link> Links { get; set; }
        public string Image { get; set; }

        public CardsModel()
        {
            Items = new List<CardItemModel>();
        }
    }
}
