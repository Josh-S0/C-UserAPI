using MongoDB.Bson.Serialization.Attributes;

namespace CSAPIProject.Models
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
        //store user password as hashByte
        [BsonElement("password")]
        public byte[] password { get; set; }
      //[BsonElement("orders")]
      //public List<Order> orders { get; set; }
    }
}
