
namespace MicroserviceCourse.Shared.Service;

public class IdentityServiceFake : IIdentityService
{
    public Guid GetUserId => Guid.Parse("2faf5b71-39a6-4e20-b519-1474207098ed");

    public string UserName => "Ahmet16";
}
