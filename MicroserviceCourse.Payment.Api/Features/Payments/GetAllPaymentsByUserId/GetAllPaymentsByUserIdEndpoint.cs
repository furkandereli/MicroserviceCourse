using MediatR;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async (IMediator mediator) => (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("GetAllPaymentsByUserId")
                .MapToApiVersion(1, 0)
                .Produces<List<GetAllPaymentsByUserIdResponse>>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
