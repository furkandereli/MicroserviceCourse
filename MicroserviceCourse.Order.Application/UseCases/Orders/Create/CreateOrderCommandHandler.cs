using MassTransit;
using MediatR;
using MicroserviceCourse.Bus.Events;
using MicroserviceCourse.Order.Application.Contracts.Refit.PaymentService;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Application.Contracts.UnitOfWork;
using MicroserviceCourse.Order.Domain.Entities;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;
using System.Net;
using OrderEntity = MicroserviceCourse.Order.Domain.Entities.Order;

namespace MicroserviceCourse.Order.Application.UseCases.Orders.Create;

public class CreateOrderCommandHandler(IOrderRepository orderRepository,
    IIdentityService identityService,
    IUnitOfWork unitOfWork,
    IPaymentService paymentService,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return ServiceResult.Error("Order items not found.", "Order must have atleast one item.", HttpStatusCode.BadRequest);

        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };

        var order = OrderEntity.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);

        foreach (var orderItem in request.Items)
        {
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
        }

        order.Address = newAddress;
        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.Code, request.Payment.CardNumber, request.Payment.CardHolderName, request.Payment.Expiration, request.Payment.Cvc, order.TotalPrice);
        var paymentResponse = await paymentService.CreateAsync(paymentRequest);

        if (paymentResponse.Status == false)
            return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);

        order.SetPaidStatus(paymentResponse.PaymentId!.Value);

        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.UserId), cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
