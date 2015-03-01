using System;
using System.Linq;
using CoreLogic;
using Machine.Specifications;

namespace MSpecTest
{
    [Subject(typeof (Order))]
    internal class When_adding_item_to_order
    {
        Establish context = () =>
            {
                _order = new Order();
                _item = new OrderDetails {Article = "Test", Count = 2, Price = 0.99};
            };

        Because of = () => _order.AddItem(_item);

        It should_add_item_to_list = () => _order.Items.Count().ShouldEqual(1);
        It should_adjust_amount = () => _order.Amount.ShouldEqual(0.99);

        static Order _order;
        static OrderDetails _item;
    }

    [Subject(typeof (Order))]
    internal class When_removing_unexisting_item
    {
        Establish context = () =>
            {
                _order = new Order();
                _item = new OrderDetails {Article = "Test", Count = 1, Price = 0.99};
            };

        Because of = () =>
            {
                _error = Catch.Exception(() => _order.RemoveItem(_item));
            };

        It should_signal_error = () => _error.ShouldBeOfExactType<OrderNotFoundException>();

        static Order _order;
        static OrderDetails _item;
        static Exception _error;
    }
}
