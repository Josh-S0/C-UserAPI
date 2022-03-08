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
        public void DeleteById(string id)
        {
            var deleteFilter = Builders<Item>.Filter.Eq("_id", id);
            itemCollection.DeleteOne(deleteFilter);
        }



    }
}
