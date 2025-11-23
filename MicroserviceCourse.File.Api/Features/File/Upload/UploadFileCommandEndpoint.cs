using MediatR;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.File.Api.Features.File.Upload;

public static class UploadFileCommandEndpoint
{
    public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (IFormFile file, IMediator mediator) => (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
            .WithName("upload")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery();

        return group;
    }
}
