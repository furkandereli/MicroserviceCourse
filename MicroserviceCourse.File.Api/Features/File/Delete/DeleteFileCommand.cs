using MicroserviceCourse.Shared;

namespace MicroserviceCourse.File.Api.Features.File.Delete;

public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
