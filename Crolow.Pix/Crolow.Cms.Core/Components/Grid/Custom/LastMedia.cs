using AutoMapper;
using Crolow.Cms.Core.Interfaces;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Media;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Components.Grid.Custom
{
    public class LastMedia : ICustomComponentBuilder
    {

        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public LastMedia(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IEnumerable<object>> GetCustomObject(CustomComponent card, BlockListModel parentProperties)
        {
            var nbItems = 5;
            var result = new List<MediaPageModel>();

            if (card.CustomProperties.Any())
            {
                var property = card.CustomProperties
                    .FirstOrDefault(p => p.Content.Value<string>("property") == "Item");
                nbItems = int.Parse(property.Content.Value<string>("value"));
            }

            using (var context = contextFactory.EnsureUmbracoContext())
            {
                var root = context.UmbracoContext.PublishedRequest.PublishedContent.Ancestor(2);
                var media = root.Descendants<MediaPage>().OrderByDescending(p => p.Date).Take(nbItems);
                result = mapper.Map<List<MediaPageModel>>(media);
            }

            return result;
        }
    }
}
