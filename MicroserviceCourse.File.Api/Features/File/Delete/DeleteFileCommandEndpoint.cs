using MediatR;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.File.Api.Features.File.Delete;

public static class DeleteFileCommandEndpoint
{
    public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/", async ([FromBody] DeleteFileCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("delete")
            .MapToApiVersion(1, 0)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);        

        return group;
    }
}
