using AutoMapper;
using Crolow.Cms.Core.Models.ViewModel.Media;
using Crolow.Cms.Generic.Core.Models.Umbraco;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid
{
    public class GridCustomLastMedia : IGridCustomComponentBuilder
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public GridCustomLastMedia(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IEnumerable<object>> GetCustomObject(GridCustomComponent card)
        {
            var nbItems = 5;
            var result = new List<MediaPageModel>();

            //if (card.CustomProperties.Any())
            //{
            //    var property = card.CustomProperties
            //        .FirstOrDefault(p => p.Content.Value<string>("property") == "Item");
            //    nbItems = int.Parse(property.Content.Value<string>("value"));
            //} 

            //using (var context = contextFactory.EnsureUmbracoContext())
            //{
            //    var root = context.UmbracoContext.PublishedRequest.PublishedContent.Ancestor(2);
            //    var media = root.Descendants<MediaPage>().OrderByDescending(p => p.Date).Take(nbItems);
            //    result = mapper.Map<List<MediaPageModel>>(media);
            //}

            return result;
        }
    }
}
