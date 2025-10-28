using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentMicroServices.Domain.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("is-deleted"), BsonRepresentation(BsonType.Boolean)]
        public bool IsDeleted { get; set; } = false;
    }
}
