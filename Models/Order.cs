using MongoDB.Bson.Serialization.Attributes;

namespace CSAPIProject.Models
{
    public class Order
    {
        [BsonId]
        public string orderId { get; set; }
        [BsonElement("orderDate")]
        public string orderDate { get; set; }
        [BsonElement("userId")]
        public string userId { get; set; }
        [BsonElement("items")]
        public List<Item> items { get; set; }
        [BsonElement("total")]
        public double total { get { return this.total; } set { CalculateOrderTotal(); } }

        public Order()
        {
            CalculateOrderTotal();
        }

        public void CalculateOrderTotal()
        {
            double total = 0;
            foreach(Item item in items)
            {
                total = total + item.itemPrice;
            }
            this.total = total; 
        }

    }
}
