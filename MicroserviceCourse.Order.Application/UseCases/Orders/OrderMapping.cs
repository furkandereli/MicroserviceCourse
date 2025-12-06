using AutoMapper;
using MicroserviceCourse.Order.Application.UseCases.Orders.Create;
using MicroserviceCourse.Order.Domain.Entities;

namespace MicroserviceCourse.Order.Application.UseCases.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
