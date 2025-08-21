using AutoMapper;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Components
{
    [ViewComponent(Name = "footer")]
    public class FooterComponent : ViewComponent
	{
		protected IMapper mapper;
		protected IUmbracoContextFactory contextFactory;
		protected IPublishedUrlProvider urlProvider;

		public FooterComponent(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
		{
			this.mapper = mapper;
			this.contextFactory = contextFactory;
			this.urlProvider = urlProvider;
		}

		public async Task<IViewComponentResult> InvokeAsync(IPublishedContent content)
		{
			var current = content.AncestorOrSelf(1);
			var footer = current.Descendant<Footer>();
			return View(footer);
		}

	}
}
