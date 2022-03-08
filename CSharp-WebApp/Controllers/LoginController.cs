using Microsoft.AspNetCore.Mvc;
using CSharp_WebApp.Models;
using CSharp_WebApp.Security;
using MongoDB.Driver;
using CSharp_WebApp.Database;

namespace CSharp_WebApp.Controllers
{
    
    public class LoginController : Controller
    {
        private UserService userService;
        private PasswordHash hash;

        public LoginController(IMongoClient client)
        {
            userService = new UserService(client);
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
        public IActionResult SignUp(UserView userV)
        {
            var user = new User();
            user.email = userV.email;
            user.firstName = userV.firstName;
            user.lastName = userV.lastName;
            user._id = Guid.NewGuid().ToString();
            hash = new PasswordHash(userV.password);
            byte[] hashBytes = hash.ToArray();
            user.password = hashBytes;
            userService.UserCollection.InsertOne(user);
            //placeholder
            return RedirectToAction("Index","Home");
        }

        //method to check hashbyte password against password
        private bool CheckPassword(byte[] hashBytes, String password)
        {
            hash = new PasswordHash(hashBytes);
            return hash.Verify(password);
        }

        //login returns user object on successful attempt
        
        public IActionResult AttemptLogin(UserView userIn)
        {
            
            var emailFilter = Builders<User>.Filter.Eq("email", userIn.email);
            var user = userService.UserCollection.Find(emailFilter).FirstOrDefault();
            if (user == null)
            {
                ViewBag.badEmail = "Email not found";
                return View("SignIn");
            }
            else if (CheckPassword(user.password, userIn.password))
            {
                Response.Cookies.Append("LoggedUserName", user.firstName);
                Response.Cookies.Append("LoggedUser",user.email);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.badPass = "Incorrect Password";
                return View("SignIn");
            }

        }

       


    }
}
