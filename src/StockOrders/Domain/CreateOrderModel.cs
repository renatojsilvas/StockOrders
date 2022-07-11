using System;

namespace StockOrders.Domain
{
    public class CreateOrderModel
    {
        public Guid? Id { get; set; } 
        public DateTime OrderDate { get; set; }
        public string Advisor { get; set; }
        public int Account { get; set; }
        public string Asset { get; set; }
        public OrderType OrderType { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public Priority Priority { get; set; }
    }
}
