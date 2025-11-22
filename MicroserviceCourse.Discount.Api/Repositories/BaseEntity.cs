using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceCourse.Discount.Api.Repositories;

public class BaseEntity
{
    public Guid Id { get; set; }
}
