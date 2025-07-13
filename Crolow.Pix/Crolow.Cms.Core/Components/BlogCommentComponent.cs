using AutoMapper;
using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.ViewModel;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Crolow.Core.Controllers.Pages;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Web;


namespace Crolow.Cms.Core.Components
{
    [ViewComponent(Name = "BlogComment")]
    public class BlogCommentComponent : ViewComponent
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;
        protected IScopeProvider scopeProvider;


        public BlogCommentComponent(IScopeProvider scopeProvider, IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
            this.scopeProvider = scopeProvider;

        }

        public async Task<IViewComponentResult> InvokeAsync(int umbracoNodeId)
        {
            using var scope = scopeProvider.CreateScope();
            var queryResults = scope.Database.Fetch<CrolowFormEntry>("SELECT * FROM CrolowForm WHERE Status=2 and BlogPostUmbracoId = @0", umbracoNodeId);
            scope.Complete();
            var comments = new CrolowFormModel { Id = umbracoNodeId, Comments = queryResults };
            return View("default", comments);
        }

    }
}
