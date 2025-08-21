using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Components
{
    [ViewComponent(Name = "navigation")]
    public class NavigationComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected ILocalizationService localizationService;

        public NavigationComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider, ILocalizationService localizationService)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.localizationService = localizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content)
        {

            var current = content.AncestorOrSelf(2);
            var newItem = mapper.Map<MenuItemModel>(current);
            AddChildren(newItem, current, 1);
            AddLanguages(newItem, content, 1);
            return View(newItem);
        }

        public void AddLanguages(MenuItemModel item, IPublishedContent current, int level)
        {
            var cur = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var root = new MenuItemModel
            {
                Active = false,
                Title = cur.ToUpper(),
                Url = current.Url(cur, UrlMode.Relative)
            };

            item.Children.Add(root);

            foreach (var culture in localizationService.GetAllLanguages())
            {
                var languageItem = new MenuItemModel
                {
                    Active = false,
                    Title = culture.CultureInfo.TwoLetterISOLanguageName.ToUpper(),
                    Url = current.Url(culture.CultureInfo.TwoLetterISOLanguageName, UrlMode.Relative)
                };
                root.Children.Add(languageItem);
            }
        }

        public void AddChildren(MenuItemModel item, IPublishedContent current, int level)
        {
            if (level < 3)
            {
                foreach (var child in current.Children)
                {
                    if (child is ISEO && !((ISEO)child).UmbracoNaviHide)
                    {
                        var newItem = mapper.Map<MenuItemModel>(child);
                        item.Children.Add(newItem);
                        AddChildren(newItem, child, level + 1);
                    }
                }
            }
        }

    }
}
