using MediatR;
using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create;

public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;