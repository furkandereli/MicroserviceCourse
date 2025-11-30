using MediatR;
using MicroserviceCourse.Payment.Api.Repositories;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Payment.Api.Features.Payments.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var payments = await context.Payments.Where(x => x.UserId == userId)
                .Select(x => new GetAllPaymentsByUserIdResponse(x.Id, x.OrderCode, x.Amount.ToString("C"), x.Created, x.Status))
                .ToListAsync();

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);   
        }
    }
}
