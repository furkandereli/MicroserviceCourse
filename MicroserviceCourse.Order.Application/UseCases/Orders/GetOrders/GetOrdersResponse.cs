using MicroserviceCourse.Order.Application.UseCases.Orders.Create;

namespace MicroserviceCourse.Order.Application.UseCases.Orders.GetOrders;

public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);
