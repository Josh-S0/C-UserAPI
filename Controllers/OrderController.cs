using CSAPIProject.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CSAPIProject.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Item> itemCollection;
        private string date = DateTime.Now.ToString("dd-MM-yyyy");
        

        public OrderController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
            itemCollection = database.GetCollection<Item>("Items");
        }

        [HttpPost("confirm")]
        public void ConfirmOrder(List<Item> items)
        {

        }




    }
}
