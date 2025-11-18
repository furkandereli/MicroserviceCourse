using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Data;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Service;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        Guid userId = identityService.GetUserId;
        var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

        var basketAtString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        Data.Basket? currentBasket;
        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAtString))
        {
            currentBasket = new Data.Basket(userId, [newBasketItem]);
            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }

        currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAtString);

        var existingBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);

        if (existingBasketItem is not null)
            currentBasket.Items.Remove(existingBasketItem);

        currentBasket.Items.Add(newBasketItem);
        await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
        return ServiceResult.SuccessAsNoContent();

    }

    private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
    {
        var basketAtString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAtString, cancellationToken);
    }
}

