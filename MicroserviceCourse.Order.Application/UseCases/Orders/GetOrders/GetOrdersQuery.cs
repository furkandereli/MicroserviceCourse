using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Order.Application.UseCases.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
