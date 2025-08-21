namespace Crolow.Cms.Core.Components.Grid
{
    public interface IGridCustomComponentBuilder
    {
        Task<IEnumerable<object>> GetCustomObject(Models.Umbraco.GridCustomComponent card);
    }
}