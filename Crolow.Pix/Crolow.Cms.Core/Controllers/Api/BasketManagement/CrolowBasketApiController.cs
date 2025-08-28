using AutoMapper;
using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.Schemas;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel.Basket;
using Crolow.Cms.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Controllers.Api.BasketManagement
{
    [Route("/CrolowBasketApi/[action]/")]
    public class CrolowBasketApiController : UmbracoApiController
    {
        private readonly Umbraco.Cms.Core.Scoping.IScopeProvider scopeProvider;
        private readonly IContentService contentService;
        private readonly ICrolowBasketService crolowBasketService;
        private readonly UmbracoHelper umbracoHelper;
        private readonly IMapper mapper;
        private readonly IMemberManager memberService;
        public CrolowBasketApiController(IMemberManager memberService,IMapper mapper, 
                            IUmbracoHelperAccessor umbracoHelperAccessor,
                            ICrolowBasketService crolowBasketService,
                            Umbraco.Cms.Core.Scoping.IScopeProvider scopeProvider, IContentService contentService)
        {
            this.scopeProvider = scopeProvider;
            this.contentService = contentService;
            this.crolowBasketService = crolowBasketService;
            this.mapper = mapper;
            umbracoHelperAccessor.TryGetUmbracoHelper(out this.umbracoHelper);
            this.memberService = memberService;
        }

        [HttpGet]
        public CrolowExtendedOrderModel GetOrder(int orderId, string clientSecret)
        {
            using var scope = scopeProvider.CreateScope();
            try
            {
                var order = crolowBasketService.GetOrder(orderId);
                var model = mapper.Map<CrolowOrderModel>(order);

                var currentMember = memberService.GetCurrentMemberAsync().Result;
                if (order.MemberId == currentMember.Id)
                {

                    foreach (var product in model.Lines)
                    {
                        var content = umbracoHelper.Content(product.ProductId);
                        mapper.Map<CrolowOrderLineModel, IPublishedContent>(product, content);
                    }

                    var finalModel = new CrolowExtendedOrderModel
                    {
                        ClientSecret = clientSecret,
                        Member = mapper.Map<CrolowMemberModel>(currentMember),
                        Order = model
                    };
                    return finalModel;
                }
            } finally { scope.Dispose(); }
            
            return null;
        }

        public void CreateOrder(CrolowOrderModel order)
        {
            using var scope = scopeProvider.CreateScope();
            try
            {
                scope.Database.InsertAsync(order);
                scope.Database.InsertBulkAsync(order.Lines);
                return;
            } finally { scope.Dispose(); }
        }

        public void UpdateOrder(CrolowOrderModel order)
        {
            using var scope = scopeProvider.CreateScope();
            try
            {
                scope.Database.UpdateAsync(order);
                foreach(var line in order.Lines)
                {
                    scope.Database.UpdateAsync(line);
                }
                return;
            }
            finally { scope.Dispose(); }
        }

    }
}
