using AutoMapper;
using MediatR;
using MicroserviceCourse.Catalog.Api.Features.Categories.Dtos;
using MicroserviceCourse.Catalog.Api.Repositories;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Extensions;
using System.Net;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.GeyById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;

public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);

        if (hasCategory == null)
            return ServiceResult<CategoryDto>.Error("Category not found", $"The category with {request.Id} not found", HttpStatusCode.NotFound);

        var categoryAsDto = mapper.Map<CategoryDto>(hasCategory);
        return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
    }
}

public static class GetCategoryByIdEndpoint
{
    public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());
        return group;
    }
}
