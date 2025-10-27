using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class Document : BaseMongoEntity
    {

        [BsonElement("file-name"), BsonRepresentation(BsonType.String)]
        public string FileName { get; set; } = default!;
        [BsonElement("file-path"), BsonRepresentation(BsonType.String)]

        public string FilePath { get; set; } = default!;
        [BsonElement("file-type"), BsonRepresentation(BsonType.String)]

        public string FileType { get; set; } = default!;
        [BsonElement("file-size"), BsonRepresentation(BsonType.Int64)]
        public long FileSize { get; set; }
    }
}
