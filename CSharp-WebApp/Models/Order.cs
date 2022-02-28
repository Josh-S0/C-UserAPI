using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace CSharp_WebApp.Models
{
    public class Order
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("orderDate")]
        public string orderDate { get; set; }
        [BsonElement("userId")]
        public string userId { get; set; }
        [BsonElement("items")]
        public List<Item> items { get; set; }
        [BsonElement("total")]
        public double total { get; set; }

        
        public void SetOrderTotal()
        {
            double total = 0;

            total = items.Sum(items => items.itemPrice);

            foreach(Item item in items)
            {
                total = total + item.itemPrice;
            }
            this.total = total; 
        }

    }
}
