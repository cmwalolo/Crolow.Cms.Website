using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Core.Models.Configuration
{
    public class SiteSettingsModel
    {
        public string Theme { get; set; }


        public IPublishedContent Root { get; set; }
        public IPublishedContent Home { get; set; }
        public IPublishedContent DataFolder { get; set; }
        public IPublishedContent ProductsFolder { get; set; }
        public IPublishedContent Footer { get; set; }
    }
}
