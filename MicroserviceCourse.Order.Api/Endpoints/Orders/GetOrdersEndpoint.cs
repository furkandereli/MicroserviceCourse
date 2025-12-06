using MediatR;
using MicroserviceCourse.Order.Application.UseCases.Orders.GetOrders;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Order.Api.Endpoints.Orders;

public static class GetOrdersEndpoint
{
    public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
            .WithName("GetOrders")
            .MapToApiVersion(1, 0)
            .Produces<ServiceResult<List<GetOrdersResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        return group;
    }
         
}
