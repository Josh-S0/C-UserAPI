using CSharp_WebApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharp_WebApp.Database
{
    public class OrderService
    {
        
        private readonly IMongoCollection<Order> orderCollection;
        
       
        public IMongoCollection<Order> OrderCollection
        {
            get { return orderCollection; }
        }
      
        public OrderService(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
          
            orderCollection = database.GetCollection<Order>("Orders");
        }

      
        public Order GetById(String id)
        {
            var idFilter = Builders<Order>.Filter.Eq("_id", id);
            return orderCollection.Find(idFilter).FirstOrDefault();
        }
       
    }
}
