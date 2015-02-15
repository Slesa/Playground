using CoreLogic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FlowTest
{
    [Binding]
    public class OrderDefinition
    {
        readonly Order _order = new Order();
        OrderDetails _item;

        [Given("I order (.*) times article (.*) of price (.*)")]
        public void GivenIOrderedAnArticle(int count, string article, double price)
        {
            _item = new OrderDetails {Count = count, Article = article, Price = price};
        }

        [When("I add the item to the order")]
        public void WhenIAddTheItemToTheOrder()
        {
            _order.AddItem(_item);
        }

        [Then("the amount should be (.*)")]
        public void ThenTheResultShouldBe(double result)
        {
            Assert.AreEqual(_order.Amount, 1.98);
        }
    }
}
