using MediatR;
using MicroserviceCourse.Basket.Api.Data;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        Data.Basket? currentBasket;
        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }

        currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

        var existingBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);

        if (existingBasketItem is not null)
            currentBasket.Items.Remove(existingBasketItem);

        currentBasket.Items.Add(newBasketItem);

        currentBasket.ApplyAvailableDiscount();

        await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
        return ServiceResult.SuccessAsNoContent();

    }  
}

