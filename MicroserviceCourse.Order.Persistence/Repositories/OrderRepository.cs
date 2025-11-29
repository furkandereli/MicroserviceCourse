using MicroserviceCourse.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
{
    public Task<List<Domain.Entities.Order>> GetOrdersByBuyerId(Guid buyerId)
    {
        return context.Orders.Include(x => x.OrderItems)
                             .Where(x => x.BuyerId == buyerId)
                             .OrderByDescending(x => x.Created)
                             .ToListAsync();
                             
    }
}
