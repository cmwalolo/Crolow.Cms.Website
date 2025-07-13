using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;


namespace Crolow.Cms.Core.Components
{
    [ViewComponent(Name = "breadcrumb")]
    public class BreadcrumbComponent : ViewComponent
	{
		protected IMapper mapper;
		protected IUmbracoContextFactory contextFactory;
		protected IPublishedUrlProvider urlProvider;

		public BreadcrumbComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
		{
			this.mapper = mapper;
			this.contextFactory = contextFactory;
			this.urlProvider = urlProvider;
		}

		public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content)
		{
			var item = new MenuItemModel();
			var current = content;
			bool active = true;
			while (current.Parent != null)
			{
				var newItem = mapper.Map<MenuItemModel>(current);
				newItem.Active = active;
				active = false;
				item.Children.Insert(0, newItem);
				current = current.Parent;
			}
			return View(item);
		}

	}
}
