namespace Crolow.Cms.Core.Models.ViewModel.Navigation
{
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            Children = new List<MenuItemModel>();
        }
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<MenuItemModel> Children { get; set; }
        public string RootUrl { get; set; }
        public bool Active { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
    }
}
