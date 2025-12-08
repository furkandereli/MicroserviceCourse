using MicroserviceCourse.Bus.Events;
using MicroserviceCourse.Discount.Api.Features.Discounts;
using MicroserviceCourse.Discount.Api.Repositories;

namespace MicroserviceCourse.Discount.Api.Consumers;

public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var discount = new Features.Discounts.Discount()
        {
            Id = NewId.NextSequentialGuid(),
            Code = DiscountCodeGenerator.GenerateDiscountCode(10),
            Created = DateTime.Now,
            Rate = 0.1f,
            Expired = DateTime.Now.AddMonths(1),
            UserId = context.Message.UserId
        };

        await appDbContext.Discounts.AddAsync(discount);
        await appDbContext.SaveChangesAsync();
    }
}
