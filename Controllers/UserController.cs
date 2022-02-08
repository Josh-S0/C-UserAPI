using CSUserAPI.Database;
using CSUserAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSUserAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController
    {
       private IMongoCollection<User> userCollection = MongoConnector.mongoDb.GetCollection<User>("Users");

        [HttpGet("/get")]
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }
        [HttpPost("/add")]
        public void AddUser()
        {
            User user = new User();
            user.firstName = "Josh";
            user.lastName = "Suchit";
            user.password = "password";
            user.UserId = Guid.NewGuid().ToString();
            userCollection.InsertOne(user);
        }



    }
}
