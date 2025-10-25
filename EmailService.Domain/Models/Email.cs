using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmailService.Domain.Models
{
    public class Email
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        [BsonElement("email_reciver"), BsonRepresentation(BsonType.String)]

        public string? To { get; set; }
        [Required]
        [BsonElement("email_subject"), BsonRepresentation(BsonType.String)]

        public string? Subject { get; set; }
        [Required]
        [BsonElement("email_body"), BsonRepresentation(BsonType.String)]

        public string? Body { get; set; }
    }
}
