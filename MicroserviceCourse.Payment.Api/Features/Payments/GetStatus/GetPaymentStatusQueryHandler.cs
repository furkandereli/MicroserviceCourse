using MediatR;
using MicroserviceCourse.Payment.Api.Repositories;
using MicroserviceCourse.Shared;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetStatus;

public class GetPaymentStatusQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
{
    public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
    {
        var payment = await appDbContext.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.OrderCode, cancellationToken);

        if(payment is null)
            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(null, false));
        
        return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(payment.Id, payment.Status == PaymentStatus.Success));
    }
}
