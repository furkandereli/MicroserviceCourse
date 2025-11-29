using AutoMapper;
using MicroserviceCourse.Order.Application.Features.Orders.Create;
using MicroserviceCourse.Order.Domain.Entities;

namespace MicroserviceCourse.Order.Application.Features.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
