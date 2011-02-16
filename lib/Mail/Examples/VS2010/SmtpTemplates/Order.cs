using System.Collections.Generic;

namespace SmtpTemplates
{
    internal class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Currency { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }
    };
}