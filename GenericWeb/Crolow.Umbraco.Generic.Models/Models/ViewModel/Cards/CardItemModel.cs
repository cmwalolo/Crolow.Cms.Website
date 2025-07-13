using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Strings;

namespace Crolow.Cms.Core.Models.ViewModel.Cards
{
    public class CardItemModel
    {
        public string CardType { get; set; }
        public string CardTemplate { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IHtmlEncodedString Body { get; set; }
        public List<Link> Links { get; set; }
        public string Image { get; set; }
        public bool DoNotCrop { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
