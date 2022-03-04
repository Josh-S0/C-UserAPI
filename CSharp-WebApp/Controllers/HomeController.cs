using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharp_WebApp.Models;
using MongoDB.Bson;
using CSharp_WebApp.Database;

namespace CSharp_WebApp.Controllers
{
   
    public class HomeController : Controller
    {
        private UserService userService;
        private ItemService itemService;
        private User LoggedUser { get; set; }

        public HomeController(IMongoClient client)
        {
            userService = new UserService(client);
            itemService = new ItemService(client);
        }

        public IActionResult Index()
        {
            var requestEmail = Request.Cookies["LoggedUser"];
            if (requestEmail!= null) {
                LoggedUser = userService.GetUserByEmail(requestEmail);
                ViewData["firstName"] = LoggedUser.firstName; 
            }
            
            var list = itemService.GetAllItems();
            ViewData["itemList"] = list;
            
            return View();
        }
       
        public IActionResult SignOut()
        {
            Response.Cookies.Delete("LoggedUser");
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
            itemService.AddItem(item);
            return RedirectToAction("AddItem","Home");
        }

        
        
       
      
        
       

        


    }
}
