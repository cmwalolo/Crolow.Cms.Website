using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Components.GlobalPageElements
{
    [ViewComponent(Name = "fixednavigation")]
    public class FixedNavigationComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected ILocalizationService localizationService;

        public FixedNavigationComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, ILocalizationService localizationService)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.localizationService = localizationService;
        }


        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content, string code = "main")
        {

            var current = content.AncestorOrSelf(1).Children<DataFolder>().FirstOrDefault()?.Children<MenuFolder>()?.FirstOrDefault()?.Children<MenuDefinition>()?.FirstOrDefault(p => p.Code == code);

            var newItem = new MenuItemModel();
            AddChildren(newItem, current, 1);

            MenuItemModel root = new MenuItemModel();
            root.Children.Add(newItem);
            root.Children.AddRange(GetDescendants(newItem).Where(p => p.Children.Any()));

            return View(root);
        }

        public IEnumerable<MenuItemModel> GetDescendants(MenuItemModel node)
        {
            foreach (var child in node.Children)
            {
                yield return child;

                foreach (var descendant in GetDescendants(child))
                {
                    yield return descendant;
                }
            }
        }

        public void AddChildren(MenuItemModel item, IPublishedContent current, int level)
        {
            foreach (var child in current.Children)
            {
                var newItem = mapper.Map<MenuItemModel>(child);
                item.Children.Add(newItem);
                if (child.Children().Any())
                {
                    AddChildren(newItem, child, level + 1);
                }
            }
        }
    }
}
