using CSharp_WebApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharp_WebApp.Database
{
    public class DBService
    {
        private readonly IMongoCollection<Item> itemCollection;
        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Order> orderCollection;
        
        public IMongoCollection<User> UserCollection
        {
            get { return userCollection; }
        }
        public IMongoCollection<Order> OrderCollection
        {
            get { return orderCollection; }
        }
        public IMongoCollection<Item> ItemCollection
        {
            get { return itemCollection; }
        }
        public DBService(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            itemCollection = database.GetCollection<Item>("Items");
            userCollection = database.GetCollection<User>("Users");
            orderCollection = database.GetCollection<Order>("Orders");
        }

        public void AddItem(Item item)
        {
            this.itemCollection.InsertOne(item);

        }
        public List<Item> GetAllItems()
        {
            return itemCollection.Find(new BsonDocument()).ToList();
        }
        public Item GetItemById(string id)
        {
            var idFilter = Builders<Item>.Filter.Eq("_id", id);
            return itemCollection.Find(idFilter).FirstOrDefault();
        }
        public void DeleteItemById(string id)
        {
            var deleteFilter = Builders<Item>.Filter.Eq("_id", id);
            itemCollection.DeleteOne(deleteFilter);
        }
        public User GetUserByEmail(string email)
        {
            var emailFilter = Builders<User>.Filter.Eq("email", email);
            var user = userCollection.Find(emailFilter).FirstOrDefault();
            return userCollection.Find(emailFilter).FirstOrDefault();
        }
        public Order GetOrderById(String id)
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
        public void DeleteUserById(String id)
        {
            var deleteFilter = Builders<User>.Filter.Eq("_id", id);
            userCollection.DeleteOne(deleteFilter);
        }
        public User GetUserByCriteria(String criteria, String query)
        {
            var criteriaFilter = Builders<User>.Filter.Eq(criteria, query);
            return userCollection.Find(criteriaFilter).FirstOrDefault();
        }
        public User GetUserById(String id)
        {
            var idFilter = Builders<User>.Filter.Eq("_id", id);
            return userCollection.Find(idFilter).FirstOrDefault();
        }
        public List<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }


    }
}
