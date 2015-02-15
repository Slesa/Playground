using System;
using CoreLogic;
using System.Linq;
using NUnit.Framework;

namespace NUnitTest
{
    [TestFixture]
    public class TestOrder
    {
        [TestCase]
        public void AddingOrderItemToOrder()
        {
            // Arrange
            var order = new Order();
            var item = new OrderDetails {Article = "Test", Count = 1, Price = 0.99};

            // Act
            order.AddItem(item);

            // Assert
            Assert.AreEqual(order.Items.Count(), 1);
            Assert.AreEqual(order.Amount, 0.99);
        }

        [TestCase]
        [ExpectedException(typeof(OrderNotFoundException))]
        public void RemoveUnexistingOrder()
        {
            // Arrange
            var order = new Order();
            var item = new OrderDetails {Article = "Test", Count = 1, Price = 0.99};
            
            // Act
            order.RemoveItem(item);

            // Assert by attribute
        }
    }
}
