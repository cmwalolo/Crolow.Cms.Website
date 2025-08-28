using Crolow.Cms.Core.Models.Schemas;
using Crolow.Cms.Core.Models.ViewModel.Basket;

namespace Crolow.Cms.Core.Services.Interfaces
{
    public interface ICrolowBasketService
    {
        string CreateOrder(CrolowOrderModel request);
        CrolowOrderSchema GetOrder(int orderId);
    }
}