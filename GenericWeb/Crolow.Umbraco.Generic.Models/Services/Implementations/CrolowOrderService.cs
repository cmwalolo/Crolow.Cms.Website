using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.Schemas;
using Crolow.Cms.Core.Models.ViewModel.Basket;
using Crolow.Cms.Core.Services.Interdaces;
using Umbraco.Cms.Core.Services;

namespace Crolow.Cms.Core.Controllers.Api.BasketManagement
{
    public class CrolowBasketService : ICrolowBasketService
    {
        private readonly Umbraco.Cms.Core.Scoping.IScopeProvider scopeProvider;
        private readonly IContentService contentService;
        private readonly ICrolowBasketService crolowBasketService;
        public CrolowBasketService(Umbraco.Cms.Core.Scoping.IScopeProvider scopeProvider, IContentService contentService)
        {
            this.scopeProvider = scopeProvider;
            this.contentService = contentService;
        }
        public CrolowOrderSchema GetOrder(int orderId)
        {
            using var scope = scopeProvider.CreateScope();
            var order = scope.Database.First<CrolowOrderSchema>("SELECT * FROM CrolowOrders WHERE Id = @0", orderId);
            if (order != null)
            {
                order.Lines = scope.Database.Fetch<CrolowOrderLineSchema>("SELECT * FROM CrolowOrderLines WHERE Id = @0", orderId);

            }
            scope.Complete();
            return order;
        }

        public string CreateOrder(CrolowOrderModel request)
        {
            long amount = 0;
            foreach (var line in request.Lines)
            {
                amount += line.UnitPrice * line.Quantity;
            }

            using var scope = scopeProvider.CreateScope();
            try
            {
                scope.Database.InsertAsync(request);
                scope.Database.InsertBulkAsync(request.Lines);
                //var paymentIntentService = new PaymentIntentService();
                //var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                //{
                //    Amount = amount,
                //    Currency = "eur",
                //    // In the latest version of the API, specifying the `automatic_payment_methods` parameter is optional because Stripe enables its functionality by default.
                //    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                //    {
                //        Enabled = true,
                //    },
                //});
                //return paymentIntent.ClientSecret; ;
                return "";
            }
            finally { scope.Dispose(); }
        }
    }
}
