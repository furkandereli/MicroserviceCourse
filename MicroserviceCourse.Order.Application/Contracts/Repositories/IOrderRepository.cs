using MicroserviceCourse.Order.Domain.Entities;
using OrderEntity = MicroserviceCourse.Order.Domain.Entities.Order;

namespace MicroserviceCourse.Order.Application.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Guid, OrderEntity>
{
    Task<List<OrderEntity>> GetOrdersByBuyerId(Guid buyerId);
    Task SetStatus(string orderCode, Guid paymentId, OrderStatus status);
}
