using static MicroserviceCourse.Web.Pages.Auth.SignUp.SignUpService;

namespace MicroserviceCourse.Web.Pages.Auth.SignUp;

public record UserCreateRequest(string Username, bool Enabled, string FirstName, string LastName, string Email, List<Credential> Credentials);