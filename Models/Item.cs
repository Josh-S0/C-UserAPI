using MongoDB.Bson.Serialization.Attributes;

namespace CSAPIProject.Models
{
    public class Item
    {
        [BsonId]
        public string itemId { get; set; }
        [BsonElement("itemPrice")]
        public double itemPrice { get; set; }
        [BsonElement("url")]
        public string url { get; set; }
        [BsonElement("stock")]
        public int stock { get; set; }

    }
}
