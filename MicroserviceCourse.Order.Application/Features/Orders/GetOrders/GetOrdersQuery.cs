using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Order.Application.Features.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
