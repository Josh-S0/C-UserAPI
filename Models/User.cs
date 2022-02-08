using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSUserAPI.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        


    }
}
