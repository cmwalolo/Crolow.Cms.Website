using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Cms.Core.Models.ViewModel
{
    public class BasePageModel
    {
        public BasePageModel(ContentModel contentModel, string theme)
        {
            Content = contentModel.Content;
            Theme = theme;
            BodyClasses = contentModel.Content.GetProperty("additionalClasses").GetValue().ToString();
        }

        public IPublishedContent Content { get; set; }
        public string Theme { get; set; }
        public string BodyClasses { get; set; }
    }
}
