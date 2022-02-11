using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSAPIProject.Models;
using MongoDB.Bson;

namespace CSAPIProject.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class HomeController : ControllerBase
    {
        private readonly IMongoCollection<Item> itemCollection;

        public HomeController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            itemCollection = database.GetCollection<Item>("Items");
        }

        [HttpPost("add")]
        public void Add(Item item)
        {
            this.itemCollection.InsertOne(item);
        }
        [HttpGet("all")]
        public List<Item> getAll()
        {
            return itemCollection.Find(new BsonDocument()).ToList();
        }
        [HttpGet("get/{id}")]
        public Item GetById(String id)
        {
            var idFilter = Builders<Item>.Filter.Eq("itemId", id);
            return itemCollection.Find(idFilter).FirstOrDefault();
        }
        [HttpDelete("delete/{id}")]
        public void DeleteById(String id)
        {
            var deleteFilter = Builders<Item>.Filter.Eq("itemId", id);
            itemCollection.DeleteOne(deleteFilter);
        }


    }
}
