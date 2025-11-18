using Asp.Versioning.Builder;
using MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceCourse.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroserviceCourse.Basket.Api.Features.Baskets.DeleteBasketItem;
using MicroserviceCourse.Basket.Api.Features.Baskets.GetBasket;
using MicroserviceCourse.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace MicroserviceCourse.Basket.Api.Features.Baskets;

public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/baskets")
           .WithTags("Baskets")
           .WithApiVersionSet(apiVersionSet)
           .GetBasketGroupItemEndpoint()
           .AddBasketItemGroupItemEndpoint()
           .DeleteBasketItemGroupItemEndpoint()
           .ApplyDiscountCouponGroupItemEndpoint()
           .RemoveDiscountCouponGroupItemEndpoint();
    }
}
