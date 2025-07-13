using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Cms.Core.Models.ViewModel
{
    public class BaseGridViewModel<T>
    {
        public IPublishedElement Content { get; set; }
        public T Model { get; set; }

        public BaseGridViewModel(IPublishedElement element, T model)
        {
            Content = element;
            Model = model;
        }
    }
}
