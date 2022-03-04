using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharp_WebApp.Models;
using MongoDB.Bson;
using CSharp_WebApp.Database;

namespace CSharp_WebApp.Controllers
{
   
    public class HomeController : Controller
    {
        private DBService dbService;
        private User LoggedUser { get; set; }

        public HomeController(IMongoClient client)
        {
            dbService = new DBService(client);
        }

        public IActionResult Index(string email)
        {
            if (email != null) {
                LoggedUser = dbService.GetUserByEmail(email);
                ViewData["firstName"] = LoggedUser.firstName; 
            }
            var list = dbService.GetAllItems();
            ViewData["itemList"] = list;
            
            return View();
        }
       
        public IActionResult SignOut()
        {
            ViewData["firstName"] = null;
            LoggedUser = null;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddItem()
        {
            return View();
        }

        public IActionResult Add(Item item)
        {
            dbService.AddItem(item);
            return RedirectToAction("AddItem","Home");
        }

        
        
       
      
        
       

        


    }
}
