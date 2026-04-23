using MicroserviceCourse.Web.Pages.Instructor.ViewModel;
using MicroserviceCourse.Web.Services.Refit;
using Refit;
using System.Text.Json;

namespace MicroserviceCourse.Web.Services;

public class CatalogService(ICatalogRefitService catalogRefitService,UserService userService, ILogger<CatalogService> logger)
{
    public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
    {
        var response = await catalogRefitService.GetCategoriesAsync();
        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            logger.LogError("Error occurred while fetching categories");
            return ServiceResult<List<CategoryViewModel>>.Error("Failed to retrieve categories. Please try again later.");
        }

        var categories = response.Content!
            .Select(c => new CategoryViewModel(c.Id, c.Name))
            .ToList();

        return ServiceResult<List<CategoryViewModel>>.Success(categories);
    }

    public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
    {
        StreamPart? pictureStreamPart = null;
        await using var stream = model.PictureFormFile?.OpenReadStream();

        if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            pictureStreamPart =
                new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);

        var response = await catalogRefitService.CreateCourseAsync(
            model.Name,
            model.Description,
            model.Price,
            pictureStreamPart,
            model.CategoryId.ToString()!
        );

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            logger.LogError("Error occurred while creating course");
            return ServiceResult.Error("Fail to create course. Please try again later");
        }

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<CourseViewModel>>> GetCoursesByUserId()
    {
        var course = await catalogRefitService.GetCoursesByUserId(userService.UserId);

        if (!course.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(course.Error.Content!);
            logger.LogError("Error occurred while fetching courses by user id");
            return ServiceResult<List<CourseViewModel>>.Error("Failed to retrieve courses. Please try again later.");
        }

        var courses = course.Content!
            .Select(c => new CourseViewModel(
                c.Id,
                c.Name,
                c.Description,
                c.Price,
                c.ImageUrl,
                c.Category.Name,
                c.Feature.Duration,
                c.Feature.Rating
            ))
            .ToList();

        return ServiceResult<List<CourseViewModel>>.Success(courses);
    }
}

