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
        private readonly IMongoCollection<Order> orderCollection;
        private string date = DateTime.Now.ToString("dd-MM-yyyy");
        

        public OrderController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
            itemCollection = database.GetCollection<Item>("Items");
            orderCollection = database.GetCollection<Order>("Orders");
        }

        [HttpPost("confirm")]
        public void ConfirmOrder(List<Item> items, String userId)
        {
            Order order = new Order();
            order._id = Guid.NewGuid().ToString();
            order.userId = userId;
            order.orderDate = date;
            order.items = items;
            order.SetOrderTotal();
            orderCollection.InsertOne(order);
            UpdateOrderList(order, userId);
        }
        [HttpPut()]
        public void UpdateOrderList(Order order, String userId)
        {
            var idFilter = Builders<User>.Filter.Eq("userId", userId);
            var user = userCollection.Find(idFilter).FirstOrDefault();
            user.orders.Add(order);
            userCollection.ReplaceOne(idFilter, user);

        }
        [HttpGet("{id}")]
        public Order GetById(String id)
        {
            var idFilter = Builders<Order>.Filter.Eq("orderId", id);
            return orderCollection.Find(idFilter).FirstOrDefault();
        }
        [HttpGet("listOrders")]
        public List<Order> GetOrdersFromUserId(String userId)
        {
            var orderFilter = Builders<User>.Filter.Eq("userId", userId);
            var user = userCollection.Find(orderFilter).FirstOrDefault();
            return user.orders;
        }


    }
}
