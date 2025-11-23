using MicroserviceCourse.Shared;

namespace MicroserviceCourse.File.Api.Features.File.Upload;

public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;
