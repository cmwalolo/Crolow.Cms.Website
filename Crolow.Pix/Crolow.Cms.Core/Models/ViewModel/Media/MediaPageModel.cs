using Crolow.Cms.Core.Models.ViewModel.Basket;

namespace Crolow.Cms.Core.Models.ViewModel.Media
{
    public class MediaPageModel
    {
        public MediaPageModel()
        {
        }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string[] Tags { get; set; }
        public string NextUrl { get; set; }
        public string PreviousUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public List<CrolowProductModel> Products { get; set; }
    }
}
