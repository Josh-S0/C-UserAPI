using CSUserAPI.Database;
using CSUserAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSUserAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private IMongoCollection<User> userCollection;

        public UserController(MongoConnector mongoConnector)
        {
           userCollection = MongoConnector.mongoDb.GetCollection<User>("Users");
        }

        [HttpGet("/get")]
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }
        [HttpGet("/get/{id}")]
        public User GetById(String id)
        {
           List<User> userList = userCollection.Find(x => x.userId == id).ToList();
            return userList.FirstOrDefault();
        }
        [HttpGet("/search")]
        public User GetByCriteria(String criteria, String query)
        {
            List<User> userList;
            var criteriaFilter = Builders<User>.Filter.Eq(criteria,query);
            userList = userCollection.Find(criteriaFilter).ToList();
            return userList.FirstOrDefault();
        }
        [HttpDelete("/delete/{id}")]
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("userId", id);
            userCollection.DeleteOne(deleteFilter);
        }
       
        [HttpPost("/add")]
        public void AddUser(User user)
        {
            user.userId = Guid.NewGuid().ToString();
            userCollection.InsertOne(user);
        }



    }
}
