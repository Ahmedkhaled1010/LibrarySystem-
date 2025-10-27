using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class BaseMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("is-deleted"), BsonRepresentation(BsonType.Boolean)]
        public bool IsDeleted { get; set; } = false;
    }
}
