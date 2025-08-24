using Crolow.Cms.Core.Models.Umbraco;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Extensions
{
    public static class CustomComponentBaseExtensions
    {
        public static T GetValue<T>(this CustomComponent component, BlockListModel properties, string key)
        {
            var property = properties?.FirstOrDefault(p => p.Content.Value<string>("property") == key);
            if (property == null)
            {
                property = component.CustomProperties?.FirstOrDefault(p => p.Content.Value<string>("property") == key);
            }

            if (property != null)
            {
                return property.Content.Value<T>("value");
            }

            return default;
        }
    }
}
