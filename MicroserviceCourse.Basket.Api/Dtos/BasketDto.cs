using System.Text.Json.Serialization;

namespace MicroserviceCourse.Basket.Api.Dtos;

public record BasketDto
{
    [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

    public List<BasketItemDto> Items { get; set; } = new();
    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }

    public decimal TotalPRice => Items.Sum(x => x.Price);
    public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

    public BasketDto() { }

    public BasketDto(List<BasketItemDto> items)
    {
        Items = items;
    }
}