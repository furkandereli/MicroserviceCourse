using MediatR;
using MicroserviceCourse.Payment.Api.Repositories;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;
using System.Net;

namespace MicroserviceCourse.Payment.Api.Features.Payments.Create;

public class CreatePaymentCommandHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
{
    public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId;
        //var userName = identityService.UserName;
        //var userRoles = identityService.Roles;

        var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

        if (!isSuccess)      
            return ServiceResult<CreatePaymentResponse>.Error("Payment failed", errorMessage!, HttpStatusCode.BadRequest);

        var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
        newPayment.SetStatus(PaymentStatus.Success);

        context.Payments.Add(newPayment);
        await context.SaveChangesAsync();

        return ServiceResult<CreatePaymentResponse>.SuccessAsOk(new CreatePaymentResponse(newPayment.Id, true, null));
    }

    private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
    {
        await Task.Delay(1000);
        return (true, null);
    }
}
