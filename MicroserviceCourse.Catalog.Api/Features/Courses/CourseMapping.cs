using MicroserviceCourse.Catalog.Api.Features.Courses.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.Dtos;

namespace MicroserviceCourse.Catalog.Api.Features.Courses;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}
