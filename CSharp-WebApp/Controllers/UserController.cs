using CSharp_WebApp.Security;
using CSharp_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharp_WebApp.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IMongoCollection<User> userCollection;
        private PasswordHash hash;

        public UserController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
        }

       
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }
        
        public User GetById(String id)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", id);
            return userCollection.Find(idFilter).FirstOrDefault();
        }
       
        public User GetByCriteria(String criteria, String query)
        {
            var criteriaFilter = Builders<User>.Filter.Eq(criteria, query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();
        }
        
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("_id", id);
            userCollection.DeleteOne(deleteFilter);
        }

    }
}
