using AutoMapper;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;


namespace Crolow.Cms.Core.Components
{
    [ViewComponent(Name = "gridLinks")]
    public class GridLinksComponent : ViewComponent
	{
		protected IMapper mapper;
		protected IUmbracoContextFactory contextFactory;
		protected IPublishedUrlProvider urlProvider;

		public GridLinksComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
		{
			this.mapper = mapper;
			this.contextFactory = contextFactory;
			this.urlProvider = urlProvider;
		}

		public async Task<IViewComponentResult> InvokeAsync(GridLinks content)
		{
			return View(content);
		}

	}
}
