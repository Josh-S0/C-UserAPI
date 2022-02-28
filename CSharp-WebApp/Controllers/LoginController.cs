using Microsoft.AspNetCore.Mvc;
using CSharp_WebApp.Models;
using CSharp_WebApp.Security;
using MongoDB.Driver;


namespace CSharp_WebApp.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IMongoCollection<User> userCollection;
        private PasswordHash hash;

        public LoginController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
        }
       
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        //stores user with hashed password in the form of hashByte
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            user._id = Guid.NewGuid().ToString();
            hash = new PasswordHash(user.tempPW);
            byte[] hashBytes = hash.ToArray();
            user.password = hashBytes;
            userCollection.InsertOne(user);
            //placeholder
            return new EmptyResult();
        }

        //method to check hashbyte password against password
        private bool CheckPassword(byte[] hashBytes, String password)
        {
            hash = new PasswordHash(hashBytes);
            return hash.Verify(password);
        }

        //login returns user object on successful attempt
        public IActionResult Login(String email, String password)
        {
            
            var emailFilter = Builders<User>.Filter.Eq("email", email);
            var user = userCollection.Find(emailFilter).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Email not found");
            }
            else if (CheckPassword(user.password, password))
            {
                return View(user);
            }
            return BadRequest("Password incorrect");

        }

       


    }
}
