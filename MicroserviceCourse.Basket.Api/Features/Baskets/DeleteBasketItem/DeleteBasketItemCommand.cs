using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
