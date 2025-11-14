
namespace MicroserviceCourse.Catalog.Api.Features.Courses.Delete;

public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var hasCourse = await context.Courses.FindAsync(request.Id);
        
        if(hasCourse is null)
        {
            return ServiceResult.ErrorAsNotFound();
        }

        context.Courses.Remove(hasCourse);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteCourseEndpoint
{
    public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (Guid Id, IMediator mediator) => 
            (await mediator.Send(new DeleteCourseCommand(Id))).ToGenericResult())
            .MapToApiVersion(1, 0)
            .WithName("DeleteCourse");

        return group;
    }
}
