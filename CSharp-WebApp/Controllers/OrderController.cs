using CSharp_WebApp.Database;
using CSharp_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CSharp_WebApp.Controllers
{
    public class OrderController : Controller
    {

        private DBService dbService;
        public OrderController(IMongoClient client)
        {
            dbService = new DBService(client);
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
            dbService.OrderCollection.InsertOne(order);
            UpdateOrderList(order, userId);
            return new EmptyResult();
        }
       
        //updates relevant orderList on User collection
        public void UpdateOrderList(Order order, String userId)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", userId);
            var user = dbService.UserCollection.Find(idFilter).FirstOrDefault();
            user.orders.Add(order._id);
            dbService.UserCollection.ReplaceOne(idFilter, user);
          

        }
      
        
    


    }
}
