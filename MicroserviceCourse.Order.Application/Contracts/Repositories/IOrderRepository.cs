using OrderEntity = MicroserviceCourse.Order.Domain.Entities.Order;

namespace MicroserviceCourse.Order.Application.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Guid, OrderEntity>
{
    Task<List<OrderEntity>> GetOrdersByBuyerId(Guid buyerId);
}
