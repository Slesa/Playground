using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Raven.HelloWorld
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public IList<OrderLine> OrderLines{ get; set; }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
    }

    public class OrderLine
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var store = new DocumentStore {Url = "http://localhost:8080"};
            store.Initialize();

            //StoreValues(store);
            ReadValues(store);

            store.DatabaseCommands.PutIndex("OrdersContainingProduct", new IndexDefinitionBuilder<Order>
                {
                    Map = orders => from order in orders from line in order.OrderLines select new {line.ProductId}
                });

            QueryValues(store);
            Console.ReadLine();
        }

        static void QueryValues(DocumentStore store)
        {
            using (var session = store.OpenSession())
            {
                var orders = session.Advanced.LuceneQuery<Order>("OrdersContainingProduct")
                    .Where("ProductId:products/1")
                    .WaitForNonStaleResults()
                    .ToArray();
                foreach (var order in orders)
                {
                    Console.WriteLine("Id: {0}", order.Id);
                    Console.WriteLine("Customer: {0}", order.Customer);
                    foreach (var orderLine in order.OrderLines)
                    {
                        Console.WriteLine("Product: {0} x {1}", orderLine.Quantity, orderLine.ProductId);
                    }
                }
            }
        }

        static void ReadValues(DocumentStore store)
        {
            using (var session = store.OpenSession())
            {
                var order = session.Load<Order>("orders/1");
                Console.WriteLine("Customer: {0}", order.Customer);
                foreach (var orderLine in order.OrderLines)
                {
                    Console.WriteLine("Products: {0} x {1}", orderLine.Quantity, orderLine.ProductId);
                }
                session.SaveChanges();
            }
        }

        static void StoreValues(DocumentStore store)
        {
            using (var session = store.OpenSession())
            {
                var product = new Product {Cost = 3.99m, Name = "Milk"};
                session.Store(product);
                session.SaveChanges();


                var order = new Order
                    {
                        Customer = "customers/Captain Future"
                        ,
                        OrderLines =
                            {
                                new OrderLine {ProductId = product.Id, Quantity = 3}
                            },
                    };

                session.Store(order);
                session.SaveChanges();
            }
        }
    }
}
