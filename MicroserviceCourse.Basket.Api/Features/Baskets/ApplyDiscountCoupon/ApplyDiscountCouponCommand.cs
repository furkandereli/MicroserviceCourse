using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
