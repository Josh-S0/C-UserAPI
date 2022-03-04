using CSharp_WebApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharp_WebApp.Database
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> itemCollection;
      
        public IMongoCollection<Item> ItemCollection
        {
            get { return itemCollection; }
        }
        public ItemService(IMongoClient client)
        {
            var database = client.GetDatabase("MainDB");
            itemCollection = database.GetCollection<Item>("Items");
           
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



    }
}
