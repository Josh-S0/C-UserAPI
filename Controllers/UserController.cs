using CSUserAPI.Models;
using CSUserAPI.Security;
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
        private PasswordHash hash;

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
            var criteriaFilter = Builders<User>.Filter.Eq(criteria, query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();
        }
        [HttpDelete("delete/{id}")]
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("userId", id);
            userCollection.DeleteOne(deleteFilter);
        }

        //stores user with hashed password in the form of hashByte
        [HttpPost("add")]
        public void AddUser(User user, String password)
        {
            user.userId = Guid.NewGuid().ToString();
            hash = new PasswordHash(password);
            byte[] hashBytes = hash.ToArray();
            user.password = hashBytes;
            userCollection.InsertOne(user);
        }

        //method to check stored hashbyte password against password
        [HttpGet]
        public bool checkPassword(byte[] hashBytes, String password)
        {
            hash = new PasswordHash(hashBytes);
            return hash.Verify(password);
        }

        //test method
        [HttpGet("test")]
        public User getUserPassword(String id, String password)
        {
            User user = GetById(id);
            if (user == null)
            {
                return null;
            }
            else if (checkPassword(user.password, password))
            {
                return user;
            }
            return null;
            
        }
    }
}
