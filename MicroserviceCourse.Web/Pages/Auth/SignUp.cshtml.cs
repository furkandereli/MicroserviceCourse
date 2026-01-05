using MicroserviceCourse.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroserviceCourse.Web.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty] public required SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;
        public void OnGet()
        {
        }
    }
}
