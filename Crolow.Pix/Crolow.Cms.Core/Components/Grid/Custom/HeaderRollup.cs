using AutoMapper;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Custom;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Components.Grid.Custom
{
    public class HeaderRollupCustom : IGridCustomComponentBuilder
    {
        protected IMapper mapper;
        protected IUmbracoContextFactory contextFactory;
        protected IPublishedUrlProvider urlProvider;

        public HeaderRollupCustom(IMapper mapper, IUmbracoContextFactory contextFactory, IPublishedUrlProvider urlProvider)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
            this.urlProvider = urlProvider;
        }

        public async Task<IEnumerable<object>> GetCustomObject(Models.Umbraco.GridCustomComponent card)
        {
            var list = new List<HeaderRollupModel>();
            if (card.CustomProperties.Any())
            {
                var property = card.CustomProperties
                    .FirstOrDefault(p => p.Content.Value<string>("property") == "Banner");
                var content = property.Content.Value<HeaderRollup>("value");
                var item = mapper.Map<HeaderRollupModel>(content);
                item.Items = new List<Umbraco.Cms.Core.Strings.IHtmlEncodedString>();

                foreach (var i in content.Items)
                {
                    item.Items.Add(((RichtextItem)i.Content).Item);
                }

                list.Add(item);
            }

            return list;
        }
    }
}
