using CSAPIProject.Models;
using CSAPIProject.Security;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSAPIProject.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> userCollection;
        private PasswordHash hash;

        public UserController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
        }

        [HttpGet("all")]
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }
        [HttpGet("get/{id}")]
        public User GetById(String id)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", id);
            return userCollection.Find(idFilter).FirstOrDefault();
        }
        [HttpGet("search")]
        public User GetByCriteria(String criteria, String query)
        {
            var criteriaFilter = Builders<User>.Filter.Eq(criteria, query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();
        }
        [HttpDelete("delete/{id}")]
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("_id", id);
            userCollection.DeleteOne(deleteFilter);
        }

    }
}
