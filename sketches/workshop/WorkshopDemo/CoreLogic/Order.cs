using System.Collections.Generic;

namespace CoreLogic
{
    public class OrderNotFoundException : KeyNotFoundException { }

    public class Order
    {
        readonly List<OrderDetails> _items = new List<OrderDetails>();

        public double Amount { get; private set; }
        public IEnumerable<OrderDetails> Items { get { return _items; } }

        public void AddItem(OrderDetails item)
        {
            _items.Add(item);
            Amount += item.Count*item.Price;
        } 

        public void RemoveItem(OrderDetails item)
        {
            if(!_items.Contains(item))
                throw new OrderNotFoundException();
            _items.Remove(item);
            Amount -= item.Count*item.Price;
        }

    }

    public class OrderDetails
    {
        public int Count { get; set; }
        public double Price { get; set; }
        public string Article { get; set; }
    }
}
