using CSharp_WebApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharp_WebApp.Database
{
    public class UserService
    {
        
        private readonly IMongoCollection<User> userCollection;
        
        public IMongoCollection<User> UserCollection
        {
            get { return userCollection; }
        }
        
        public UserService(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
        }
       
        public User GetUserByEmail(string email)
        {
            var emailFilter = Builders<User>.Filter.Eq("email", email);
            var user = userCollection.Find(emailFilter).FirstOrDefault();
            return userCollection.Find(emailFilter).FirstOrDefault();
        }
     
        public List<string> GetOrdersFromUserId(String userId)
        {
            var orderFilter = Builders<User>.Filter.Eq("_id", userId);
            var user = userCollection.Find(orderFilter).FirstOrDefault();
            return user.orders;
        }
        public void DeleteUserById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("_id", id);
            userCollection.DeleteOne(deleteFilter);
        }
        public User GetUserByCriteria(String criteria, String query)
        {
            var criteriaFilter = Builders<User>.Filter.Eq(criteria, query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();
        }
        public User GetUserById(String id)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", id);
            return userCollection.Find(idFilter).FirstOrDefault();
        }
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }


    }
}
