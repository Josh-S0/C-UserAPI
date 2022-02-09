using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSUserAPI.Models
{
    public class User
    {

        [BsonId]
        public string userId { get; set; }
        [BsonElement("firstName")]
        public string firstName { get; set; }
        [BsonElement("lastName")]
        public string lastName { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("password")]
        public string password { get; set; }

        
    }
}
