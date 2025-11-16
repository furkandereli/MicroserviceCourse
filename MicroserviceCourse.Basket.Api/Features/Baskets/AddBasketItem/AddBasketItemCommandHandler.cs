using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.NewGuid();
        var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

        var basketAtString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        BasketDto? currentBasket;
        var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAtString))
        {
            currentBasket = new BasketDto(userId, [newBasketItem]);
            await CreateCache(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }

        currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAtString);

        var existingBasketItem = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);

        if (existingBasketItem is not null)
            currentBasket.BasketItems.Remove(existingBasketItem);

        currentBasket.BasketItems.Add(newBasketItem);
        await CreateCache(currentBasket, cacheKey, cancellationToken);
        return ServiceResult.SuccessAsNoContent();

    }

    private async Task CreateCache(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
    {
        var basketAtString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAtString, cancellationToken);
    }
}

