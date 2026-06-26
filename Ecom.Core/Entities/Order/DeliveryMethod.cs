namespace Ecom.Core.Entities.Order
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string name, decimal price, string deliveryTime, string description)
        {
            Name = name;
            this.price = price;
            DeliveryTime = deliveryTime;
            Description = description;
        }

        public string Name { get; set; }
        public decimal price { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
    
   
    }
}