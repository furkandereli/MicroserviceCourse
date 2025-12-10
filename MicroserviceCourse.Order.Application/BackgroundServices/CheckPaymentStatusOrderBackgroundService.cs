using MicroserviceCourse.Order.Application.Contracts.Refit.PaymentService;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Application.Contracts.UnitOfWork;
using MicroserviceCourse.Order.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicroserviceCourse.Order.Application.BackgroundServices;

public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
        var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var orders = orderRepository.Where(x => x.Status == OrderStatus.WaitingForPayment).ToList();

            foreach (var order in orders)
            {
                var paymentStatusResponse = await paymentService.GetStatusAsync(order.Code);
                if (paymentStatusResponse.IsPaid!)
                {
                    await orderRepository.SetStatus(order.Code, paymentStatusResponse.PaymentId!.Value, OrderStatus.Paid);
                    await unitOfWork.CommitAsync(stoppingToken);
                }
            }
            await Task.Delay(2000, stoppingToken);
        }
        
    }
}
