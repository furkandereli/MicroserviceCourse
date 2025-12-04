using Asp.Versioning.Builder;
using MicroserviceCourse.File.Api.Features.File.Delete;
using MicroserviceCourse.File.Api.Features.File.Upload;

namespace MicroserviceCourse.File.Api.Features.File;

public static class FileEndpointExt
{
    public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("/api/v{version:apiVersion}/files")
            .WithTags("files")
            .WithApiVersionSet(apiVersionSet)
            .UploadFileGroupItemEndpoint()
            .DeleteFileGroupItemEndpoint()
            .RequireAuthorization();
    }
}
