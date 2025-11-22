using MicroserviceCourse.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Discount.Api.Features.Discounts.CreateDiscount;

public static class GetDiscountByCodeQueryEndpoint
{
    public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateDiscount")
            .MapToApiVersion(1, 0)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateDiscountCommandValidator>>();

        return group;
    }
}
