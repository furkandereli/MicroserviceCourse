using AutoMapper;
using MediatR;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Application.UseCases.Orders.Create;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;

namespace MicroserviceCourse.Order.Application.UseCases.Orders.GetOrders;

public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{
    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByBuyerId(identityService.UserId);

        var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();

        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}
