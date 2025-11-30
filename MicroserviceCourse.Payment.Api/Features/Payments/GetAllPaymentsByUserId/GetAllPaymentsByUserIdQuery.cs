using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetAllPaymentsByUserId;

public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;