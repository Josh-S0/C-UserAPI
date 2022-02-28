using Microsoft.AspNetCore.Mvc;
using CSAPIProject.Models;
using CSAPIProject.Security;
using MongoDB.Driver;

namespace CSAPIProject.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMongoCollection<User> userCollection;
        private PasswordHash hash;

        public LoginController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
        }

        //stores user with hashed password in the form of hashByte
        [HttpPost("add")]
        public void AddUser(User user, String password)
        {
            user._id = Guid.NewGuid().ToString();
            hash = new PasswordHash(password);
            byte[] hashBytes = hash.ToArray();
            user.password = hashBytes;
            userCollection.InsertOne(user);
        }

        //method to check hashbyte password against password
        [HttpGet]
        public bool CheckPassword(byte[] hashBytes, String password)
        {
            hash = new PasswordHash(hashBytes);
            return hash.Verify(password);
        }

        //login returns user object on successful attempt
        [HttpGet("auth")]
        public User Login(String email, String password)
        {
            
            var emailFilter = Builders<User>.Filter.Eq("email", email);
            var user = userCollection.Find(emailFilter).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else if (CheckPassword(user.password, password))
            {
                return user;
            }
            return null;

        }


    }
}
