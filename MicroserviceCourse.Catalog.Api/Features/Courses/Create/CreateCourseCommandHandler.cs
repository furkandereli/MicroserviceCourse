using MicroserviceCourse.Bus.Commands;

namespace MicroserviceCourse.Catalog.Api.Features.Courses.Create;

public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (!hasCategory)
            return ServiceResult<Guid>.Error("Category not found.", $"Category with id {request.CategoryId} was not found.", HttpStatusCode.NotFound);

        var hasCourse = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);

        if (hasCourse)
            return ServiceResult<Guid>.Error("Course already exists.", $"Course with name {request.Name} already exists.", HttpStatusCode.BadRequest);

        var newCourse = mapper.Map<Course>(request);
        newCourse.Created = DateTime.Now;
        newCourse.Id = NewId.NextSequentialGuid();

        newCourse.Feature = new Feature()
        {
            Duration = 10,
            Rating = 0,
            EducatorFullName = "Ahmet Yılmaz"
        };

        context.Courses.Add(newCourse);
        await context.SaveChangesAsync(cancellationToken);

        if (request.Picture is not null)
        {
            using var memoryStream = new MemoryStream();
            await request.Picture.CopyToAsync(memoryStream, cancellationToken);

            var pictureAsByteArray = memoryStream.ToArray();
            UploadCoursePictureCommand uploadCoursePictureCommand = new UploadCoursePictureCommand(newCourse.Id, pictureAsByteArray, request.Picture.FileName);

            await publishEndpoint.Publish(uploadCoursePictureCommand, cancellationToken);
        }

        return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
    }
}
