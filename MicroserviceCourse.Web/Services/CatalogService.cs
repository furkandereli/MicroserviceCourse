using MicroserviceCourse.Web.Pages.Instructor.ViewModel;
using MicroserviceCourse.Web.Services.Refit;
using Refit;
using System.Text.Json;

namespace MicroserviceCourse.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService,TokenService tokenService, ILogger<CatalogService> logger)
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
    }
}
