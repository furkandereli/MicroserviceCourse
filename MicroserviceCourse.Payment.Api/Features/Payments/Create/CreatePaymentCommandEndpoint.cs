using MediatR;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Payment.Api.Features.Payments.Create;

public static class GetAllPaymentsByUserIdQueryEndpoint
{
    public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", async ([FromBody] CreatePaymentCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
           .WithName("CreatePayment")
           .MapToApiVersion(1, 0)
           .Produces(StatusCodes.Status204NoContent)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
           .AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>();

        return group;
    }
}
