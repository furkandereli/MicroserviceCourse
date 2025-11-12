using MicroserviceCourse.Catalog.Api.Features.Courses.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetAll;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetById;

namespace MicroserviceCourse.Catalog.Api.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/courses")
            .WithTags("Courses")
            .CreateCourseGroupItemEndpoint()
            .GetAllCoursesGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint();
    }
}
