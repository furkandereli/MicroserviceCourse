using MicroserviceCourse.Web.Pages.Instructor.ViewModel;
using MicroserviceCourse.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroserviceCourse.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; } = null!;
        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserId();
            //if(result.IsFail) {}
            CourseViewModels = result.Data!;
        }
    }
}
