using Asp.Versioning.Builder;
using MicroserviceCourse.Payment.Api.Features.Payments.Create;
using MicroserviceCourse.Payment.Api.Features.Payments.GetAllPaymentsByUserId;
using MicroserviceCourse.Payment.Api.Features.Payments.GetStatus;

namespace MicroserviceCourse.Payment.Api.Features.Payments;


public static class PaymentEndpointExt
{
    public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/payments")
            .WithTags("payments")
            .WithApiVersionSet(apiVersionSet)
            .CreatePaymentGroupItemEndpoint()
            .GetAllPaymentsByUserIdGroupItemEndpoint()
            .GetPaymentStatusGroupItemEndpoint();
    }
}
