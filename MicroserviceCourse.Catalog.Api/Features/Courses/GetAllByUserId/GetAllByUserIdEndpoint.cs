using MicroserviceCourse.Catalog.Api.Features.Courses.Dtos;

namespace MicroserviceCourse.Catalog.Api.Features.Courses.GetAllByUserId;

public record GetAllByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

public class GetAllByUserIdRequestHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllByUserIdQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
        var courses = await context.Courses.Where(x => x.UserId == request.Id).ToListAsync(cancellationToken);
        var categories = await context.Categories.ToListAsync(cancellationToken);

        foreach (var course in courses)
        {
            course.Category = categories.First(x => x.Id == course.CategoryId);
        }

        var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
    }
}

public static class GetAllByUserIdEndpoint
{
    public static RouteGroupBuilder GetAllByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user/{userId:guid}", async (Guid userId, IMediator mediator) => (await mediator.Send(new GetAllByUserIdQuery(userId))).ToGenericResult())
            .MapToApiVersion(1, 0)
            .WithName("GetAllByUserId");

        return group;
    }
}
