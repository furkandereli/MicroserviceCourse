using System.Text.Json.Serialization;

namespace MicroserviceCourse.Basket.Api.Dtos;

public record BasketDto
{
    [JsonIgnore] public Guid UserId { get; init; }
    public List<BasketItemDto> Items { get; set; } = new();

    public BasketDto() { }

    public BasketDto(Guid userId, List<BasketItemDto> items)
    {
        UserId = userId;
        Items = items;
    }
}