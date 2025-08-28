namespace Crolow.Cms.Core.Interfaces
{
    public interface ICachableComponentModel
    {
        int OriginId { get; set; }
        long OriginVersion { get; set; }

    }
}