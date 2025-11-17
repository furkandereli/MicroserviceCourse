namespace MicroserviceCourse.Shared.Service;

public interface IIdentityService
{
    public Guid GetUserId { get; }
    public string UserName { get; }
}
