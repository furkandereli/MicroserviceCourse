using MicroserviceCourse.Payment.Api.Repositories;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetAllPaymentsByUserId;

public record GetAllPaymentsByUserIdResponse(Guid Id, string orderCode, string Amount, DateTime Created, PaymentStatus Status);