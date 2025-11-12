using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Features.Categories.GetAll;
using MicroserviceCourse.Catalog.Api.Features.Categories.GeyById;

namespace MicroserviceCourse.Catalog.Api.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/categories")
            .WithTags("Categories")
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoriesGroupItemEndpoint()
            .GetByIdCategoryGroupItemEndpoint();
    }
}
