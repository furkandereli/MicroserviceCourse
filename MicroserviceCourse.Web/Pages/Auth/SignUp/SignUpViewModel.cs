using System.ComponentModel.DataAnnotations;

namespace MicroserviceCourse.Web.Pages.Auth.SignUp;

public record SignUpViewModel
{
    [Display(Name = "First Name:")]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name:")]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Display(Name = "User Name:")]
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [Display(Name = "Email:")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Display(Name = "Password:")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Display(Name = "Password Confirm:")]
    [Required(ErrorMessage = "Password confirm is required")]
    [Compare(nameof(Password), ErrorMessage = "The password don't match")]
    public string PasswordConfirm { get; set; }

    public static SignUpViewModel Empty => new()
    {
        FirstName = string.Empty,
        LastName = string.Empty,
        UserName = string.Empty,
        Email = string.Empty,
        Password = string.Empty,
        PasswordConfirm = string.Empty
    };

    public static SignUpViewModel GetExampleModel => new()
    {
        FirstName = "Ahmet",
        LastName = "Yıldız",
        UserName = "ahmetyildiz",
        Email = "ahmet@outlook.com",
        Password = "Password123.",
        PasswordConfirm = "Password123."
    };
}
