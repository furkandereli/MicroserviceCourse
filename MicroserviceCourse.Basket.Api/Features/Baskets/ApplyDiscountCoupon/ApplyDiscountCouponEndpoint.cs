using MediatR;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public static class ApplyDiscountCouponEndpoint
{
    public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/applyDiscountCoupon", async (ApplyDiscountCouponCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("ApplyDiscount")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

        return group;
    }
}
