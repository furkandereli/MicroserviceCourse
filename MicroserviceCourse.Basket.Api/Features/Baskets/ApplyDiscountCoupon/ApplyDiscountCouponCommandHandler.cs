using MediatR;
using MicroserviceCourse.Shared;
using System.Net;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
            return ServiceResult.Error("Basket not found.", HttpStatusCode.NotFound);

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

        if(!basket.Items.Any())
            return ServiceResult.Error("Basket item not found.", HttpStatusCode.NotFound);

        basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

        basketAsJson = JsonSerializer.Serialize(basket);
        await basketService.CreateBasketCacheAsync(basket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
