using MediatR;
using MicroserviceCourse.Order.Application.Features.Orders.Create;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Order.Api.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateOrder")
            .MapToApiVersion(1, 0)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

        return group;
    }
}
