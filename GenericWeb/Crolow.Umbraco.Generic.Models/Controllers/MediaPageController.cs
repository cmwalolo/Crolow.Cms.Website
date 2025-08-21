using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Basket;
using Crolow.Cms.Core.Models.ViewModel.Media;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace UmbracoProject.Controller
{
    [ResponseCache(NoStore = false, Duration = 3600, Location = ResponseCacheLocation.Client)]
    public class MediaPageController : BaseMvcController
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        public MediaPageController(IPublishedUrlProvider urlProvider, IMapper mapper, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            this.mapper = mapper;
            this.urlProvider = urlProvider;
        }

        public override IActionResult Index()
        {
            var item = mapper.Map<MediaPageModel>(CurrentPage);

            var root = this.UmbracoContext.Content.GetAtRoot();
            var products = root.DescendantsOrSelf<Product>();
            item.Products = mapper.Map<List<CrolowProductModel>>(products);

           // var collection = CurrentPage.Parent<MediaCollectionPage>();

            this.ViewData["Extra"] = item;
            var children = CurrentPage.Parent.Children.OrderByDescending(p => p.Value<DateTime>("date")).ToList();
            var index = children.IndexOf(CurrentPage);
            if (index != -1)
            {
                if (index > 0)
                {
                    item.PreviousUrl = children[index - 1].Url();
                }
                if (index < children.Count - 1)
                {
                    item.NextUrl = children[index + 1].Url();
                }
            }
            return CurrentTemplate(CurrentPage);
        }

    }
}