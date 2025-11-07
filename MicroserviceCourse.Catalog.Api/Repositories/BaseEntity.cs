using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceCourse.Catalog.Api.Repositories;

public class BaseEntity
{
    [BsonElement("_id")]
    public Guid Id { get; set; }
}
