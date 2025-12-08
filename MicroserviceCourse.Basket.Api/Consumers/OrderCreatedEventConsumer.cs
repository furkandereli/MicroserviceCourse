using MassTransit;
using MicroserviceCourse.Basket.Api.Features.Baskets;
using MicroserviceCourse.Bus.Events;

namespace MicroserviceCourse.Basket.Api.Consumers;

public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
        await basketService.DeleteBasket(context.Message.UserId);
    }
}
