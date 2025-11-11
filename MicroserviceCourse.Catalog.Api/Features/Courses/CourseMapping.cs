using MicroserviceCourse.Catalog.Api.Features.Courses.Create;

namespace MicroserviceCourse.Catalog.Api.Features.Courses;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>().ReverseMap();
    }
}
