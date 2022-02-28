using CSharp_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CSharp_WebApp.Controllers
{
    public class OrderController : Controller
    {

        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Order> orderCollection;
 
        

        public OrderController(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            userCollection = database.GetCollection<User>("Users");
            orderCollection = database.GetCollection<Order>("Orders");
        }

        
        public IActionResult ConfirmOrder(List<Item> items, String userId)
        {
            Order order = new Order
            {
                _id = Guid.NewGuid().ToString(),
                userId = userId,
                orderDate = DateTime.Now.ToString("dd-MM-yyyy"),
                items = items
            };
            order.SetOrderTotal();
            orderCollection.InsertOne(order);
            UpdateOrderList(order, userId);
            return new EmptyResult();
        }
       
        //updates relevant orderList on User collection
        public IActionResult UpdateOrderList(Order order, String userId)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", userId);
            var user = userCollection.Find(idFilter).FirstOrDefault();
            user.orders.Add(order._id);
            userCollection.ReplaceOne(idFilter, user);
            return new EmptyResult();

        }
        public Order GetById(String id)
        {
            var idFilter = Builders<Order>.Filter.Eq("_id", id);
            return orderCollection.Find(idFilter).FirstOrDefault();
        }
        
        public List<string> GetOrdersFromUserId(String userId)
        {
            var orderFilter = Builders<User>.Filter.Eq("_id", userId);
            var user = userCollection.Find(orderFilter).FirstOrDefault();
            return user.orders;
        }


    }
}
