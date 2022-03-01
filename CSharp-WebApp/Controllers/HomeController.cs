using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharp_WebApp.Models;
using MongoDB.Bson;

namespace CSharp_WebApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IMongoCollection<Item> itemCollection;
        private readonly IMongoCollection<User> userCollection;
        private User LoggedUser { get; set; }

        public HomeController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            itemCollection = database.GetCollection<Item>("Items");
            userCollection = database.GetCollection<User>("Users");
        }

        public IActionResult Index(string email)
        {
            if (email != null) {
                LoggedUser = GetByEmail(email);
                ViewData["firstName"] = LoggedUser.firstName; 
            }
            var list = GetAll();
            ViewData["itemList"] = list;
            
            return View();
        }
       

        public IActionResult AddItem()
        {
            return View();
        }

        public void Add(Item item)
        {
            this.itemCollection.InsertOne(item);
            
        }
        
        public List<Item> GetAll()
        {
            return itemCollection.Find(new BsonDocument()).ToList();
        }
      
        public Item GetById(string id)
        {
            var idFilter = Builders<Item>.Filter.Eq("_id", id);
            return itemCollection.Find(idFilter).FirstOrDefault();
        }
        public User GetByEmail(string email)
        {
            var emailFilter = Builders<User>.Filter.Eq("email", email);
            var user = userCollection.Find(emailFilter).FirstOrDefault();
            return userCollection.Find(emailFilter).FirstOrDefault();
        }

        public void DeleteById(string id)
        {
            var deleteFilter = Builders<Item>.Filter.Eq("_id", id);
            itemCollection.DeleteOne(deleteFilter);
        }


    }
}
