using MicroserviceCourse.Catalog.Api.Features.Courses.Dtos;

namespace MicroserviceCourse.Catalog.Api.Features.Courses.GetAll;

public record GetAllCoursesQuery : IRequestByServiceResult<List<CourseDto>>;

public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await context.Courses.AsNoTracking().ToListAsync(cancellationToken);
        
        
        var categories = await context.Categories.AsNoTracking().ToListAsync(cancellationToken);

        foreach (var course in courses)
        {
            course.Category = categories.First(c => c.Id == course.CategoryId);
        }

        var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
    }
}

public static class GetAllCoursesEndpoint
{
    public static RouteGroupBuilder GetAllCoursesGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
                    (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                    .MapToApiVersion(1, 0)
                    .WithName("GetAllCourses");

        return group;
    }
}
