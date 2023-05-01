using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LibraryService.Models
{
    public class BookDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        [BsonElement("author")]
        public string Author { get; set; } = null!;
        [BsonElement("country")]
        public string Country { get; set; } = null!;
        [BsonElement("imageLink")]
        public string ImageLink { get; set; } = null!;
        [BsonElement("language")]
        public string Language { get; set; } = null!;
        [BsonElement("link")]
        public string Link { get; set; } = null!;
        [BsonElement("pages")]
        public int Pages { get; set; }
        [BsonElement("title")]
        public string Title { get; set; } = null!;
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("stockLeft")]
        public int StockLeft { get; set; }
        [BsonElement("price")]
        public int Price { get; set; }

    }
}
