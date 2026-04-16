using MicroserviceCourse.Web.Pages.Instructor.ViewModel;
using MicroserviceCourse.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroserviceCourse.Web.Pages.Instructor;

[Authorize(Roles = "instructor")]
public class CreateCourseModel(CatalogService catalogService) : PageModel
{
    public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;

    public async Task OnGet()
    {
        var categoriesResult = await catalogService.GetCategoriesAsync();
        if (categoriesResult.IsFail)
        {
            //Redirect to error page
        }

        ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
    }
}
