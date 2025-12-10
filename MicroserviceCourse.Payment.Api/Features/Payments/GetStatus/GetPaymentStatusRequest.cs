using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetStatus;

public record GetPaymentStatusRequest(string OrderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;
