using CSUserAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSUserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> userCollection;

        public UserController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
           userCollection = database.GetCollection<User>("Users");
        }

        [HttpGet("get")]
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }
        [HttpGet("get/{id}")]
        public User GetById(String id)
        {
            var idFilter = Builders<User>.Filter.Eq("userId", id);
            return userCollection.Find(idFilter).FirstOrDefault();
        }
        [HttpGet("search")]
        public User GetByCriteria(String criteria, String query)
        {
            var criteriaFilter = Builders<User>.Filter.Eq(criteria,query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();       
        }
        [HttpDelete("delete/{id}")]
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("userId", id);
            userCollection.DeleteOne(deleteFilter);
        }
       
        [HttpPost("add")]
        public void AddUser(User user)
        {
            user.userId = Guid.NewGuid().ToString();
            userCollection.InsertOne(user);
        }



    }
}
