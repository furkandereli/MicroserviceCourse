using Asp.Versioning.Builder;
using MicroserviceCourse.Catalog.Api.Features.Courses.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.Delete;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetAll;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetAllByUserId;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetById;
using MicroserviceCourse.Catalog.Api.Features.Courses.Update;

namespace MicroserviceCourse.Catalog.Api.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/courses")
            .WithApiVersionSet(apiVersionSet)
            .WithTags("Courses")
            .CreateCourseGroupItemEndpoint()
            .GetAllCoursesGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoint()
            .DeleteCourseGroupItemEndpoint()
            .GetAllByUserIdGroupItemEndpoint();
    }
}
