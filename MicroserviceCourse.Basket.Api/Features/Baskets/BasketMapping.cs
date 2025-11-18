using AutoMapper;
using MicroserviceCourse.Basket.Api.Data;
using MicroserviceCourse.Basket.Api.Dtos;

namespace MicroserviceCourse.Basket.Api.Features.Baskets;

public class BasketMapping : Profile
{
    public BasketMapping()
    {
        CreateMap<BasketDto, Data.Basket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
    }
}
