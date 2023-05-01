using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LibraryService.Models
{
    public class NotificationDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public bool MarkAsRead { get; set;}
    }
}
