using MassTransit;
using MediatR;
using MicroserviceCourse.Catalog.Api.Repositories;
using MicroserviceCourse.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create;

public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
{
    public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existCategory = await context.Categories.AnyAsync(x => x.Name == request.name, cancellationToken);

        if (existCategory)
        {
            return ServiceResult<CreateCategoryResponse>.Error("Category name already exists.", $"Category name '{request.name}' already exists.", HttpStatusCode.BadRequest);
        }

        var category = new Category
        {
            Name = request.name,
            Id = NewId.NextSequentialGuid()
        };

        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "<empty>");
    }
}
