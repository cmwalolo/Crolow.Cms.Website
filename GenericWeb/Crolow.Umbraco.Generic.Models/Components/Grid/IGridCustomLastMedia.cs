using Crolow.Cms.Generic.Core.Models.Umbraco;
using Microsoft.AspNetCore.Mvc;

namespace Crolow.Cms.Core.Components.Grid
{
    public interface IGridCustomComponentBuilder
    {
        Task<IEnumerable<object>> GetCustomObject(GridCustomComponent card);
    }
}