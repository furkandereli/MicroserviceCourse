using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Shared.Service;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAtString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAtString, cancellationToken);
        }
    }
}
