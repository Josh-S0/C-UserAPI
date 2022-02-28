using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharp_WebApp.Models;
using MongoDB.Bson;

namespace CSharp_WebApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IMongoCollection<Item> itemCollection;
        
        public HomeController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            itemCollection = database.GetCollection<Item>("Items");
        }

        public IActionResult Index()
        {
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
      
        public Item GetById(String id)
        {
            var idFilter = Builders<Item>.Filter.Eq("_id", id);
            return itemCollection.Find(idFilter).FirstOrDefault();
        }
       
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<Item>.Filter.Eq("_id", id);
            itemCollection.DeleteOne(deleteFilter);
        }


    }
}
