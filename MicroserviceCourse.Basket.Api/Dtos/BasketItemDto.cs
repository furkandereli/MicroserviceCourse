namespace MicroserviceCourse.Basket.Api.Dtos;

public record BasketItemDto(Guid Id,
    string Name,
    string ImageUrl,
    decimal Price,
    decimal? PriceByApplyDiscountRate);