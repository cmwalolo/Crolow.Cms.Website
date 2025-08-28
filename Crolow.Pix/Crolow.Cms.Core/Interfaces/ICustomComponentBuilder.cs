using Crolow.Cms.Core.Models.Umbraco;
using Umbraco.Cms.Core.Models.Blocks;

namespace Crolow.Cms.Core.Interfaces
{
    public interface ICustomComponentBuilder
    {
        Task<IEnumerable<object>> GetCustomObject(CustomComponent card, BlockListModel parentProperties);
    }
}