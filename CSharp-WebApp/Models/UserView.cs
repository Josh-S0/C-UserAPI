using MongoDB.Bson.Serialization.Attributes;

namespace CSharp_WebApp.Models
{
    public class UserView
    {
                             
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password{ get; set; }
        
    }
}
