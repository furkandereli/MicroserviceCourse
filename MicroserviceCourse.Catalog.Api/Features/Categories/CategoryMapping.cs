using AutoMapper;
using MicroserviceCourse.Catalog.Api.Features.Categories.Dtos;

namespace MicroserviceCourse.Catalog.Api.Features.Categories;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
