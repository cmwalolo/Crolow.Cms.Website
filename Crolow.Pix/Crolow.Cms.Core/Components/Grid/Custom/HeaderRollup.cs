using AutoMapper;
using Crolow.Cms.Core.Extensions;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Custom;
using Crolow.Cms.Core.Services.Interfaces;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Crolow.Cms.Core.Components.Grid.Custom
{


    public class HeaderRollupCustom : ICustomComponentBuilder
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

        public async Task<IEnumerable<object>> GetCustomObject(CustomComponent card, BlockListModel parentProperties)
        {
            var list = new List<HeaderRollupModel>();
            if (card.CustomProperties.Any())
            {
                var content = card.GetValue<HeaderRollup>(parentProperties, "Banner");
                if (content != null)
                {
                    var item = mapper.Map<HeaderRollupModel>(content);
                    item.Items = new List<Umbraco.Cms.Core.Strings.IHtmlEncodedString>();

                    foreach (var i in content.Items)
                    {
                        item.Items.Add(((RichtextItem)i.Content).Item);
                    }

                    list.Add(item);
                }
            }

            return list;
        }
    }
}
