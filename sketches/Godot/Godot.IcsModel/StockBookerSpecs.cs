using System;
using System.Linq;
using Godot.IcsModel.Entities;
using Godot.Model;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsModel
{
    [Subject(typeof(StockBooker))]
    public class When_calling_bookitemsintostock_with_null_params
    {
        Establish context = () =>
            {
                var unitConverter = MockRepository.GenerateMock<IUnitConverter>();
                _stockBooker = new StockBooker(unitConverter);
            };

        Because of = () =>
            {
                _stockError = Catch.Exception(
                    () => _stockBooker.BookItemIntoStock(null, 0m, new Unit(), new RecipeableItem()));
                _unitError = Catch.Exception(
                    () => _stockBooker.BookItemIntoStock(new Stock(), 0m, null, new RecipeableItem()));
                _recipeableItemError = Catch.Exception(
                    () => _stockBooker.BookItemIntoStock(new Stock(), 0m, new Unit(), null));
            };

        It should_fail_with_stock_null = () => _stockError.ShouldBeOfType<ArgumentNullException>();
        It should_fail_with_unit_null = () => _unitError.ShouldBeOfType<ArgumentNullException>();
        It should_fail_with_recipeableitem_null = () => _recipeableItemError.ShouldBeOfType<ArgumentNullException>();

        static IStockBooker _stockBooker;
        static Exception _stockError;
        static Exception _unitError;
        static Exception _recipeableItemError;
    };

    [Subject(typeof(StockBooker))]
    public class When_calling_bookitemsoutofstock_with_null_params
    {
        Establish context = () =>
            {
                var unitConverter = MockRepository.GenerateMock<IUnitConverter>();
                _stockBooker = new StockBooker(unitConverter);
            };

        Because of = () =>
        {
            _stockError = Catch.Exception(
                () => _stockBooker.BookItemOutOfStock(null, 0m, new Unit(), new RecipeableItem()));
            _unitError = Catch.Exception(
                () => _stockBooker.BookItemOutOfStock(new Stock(), 0m, null, new RecipeableItem()));
            _recipeableItemError = Catch.Exception(
                () => _stockBooker.BookItemOutOfStock(new Stock(), 0m, new Unit(), null));
        };

        It should_fail_with_stock_null = () => _stockError.ShouldBeOfType<ArgumentNullException>();
        It should_fail_with_unit_null = () => _unitError.ShouldBeOfType<ArgumentNullException>();
        It should_fail_with_recipeableitem_null = () => _recipeableItemError.ShouldBeOfType<ArgumentNullException>();

        static IStockBooker _stockBooker;
        static Exception _stockError;
        static Exception _unitError;
        static Exception _recipeableItemError;
    };

    [Subject(typeof(StockBooker))]
    public class When_booking_existing_stockitem_into_stock
    {
        Establish context = () =>
        {
            _unit = new Unit();
            _stock = new Stock();
            _purchaseItem = new PurchaseItem();
            _stock.AddStockItem(new StockItem(42m, _purchaseItem){Unit = _unit});
            _stockBooker = new StockBooker(new UnitConverter());
        };

        Because of = () => _foundItem = _stockBooker.BookItemIntoStock(_stock, 0.42m, _unit, _purchaseItem);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);
        It should_have_right_quantity = () => _foundItem.Quantity.ShouldEqual(42.42m);

        static IStockBooker _stockBooker;
        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
        static Unit _unit;
    }

    [Subject(typeof(StockBooker))]
    public class When_booking_existing_stockitem_out_of_stock
    {
        Establish context = () =>
        {
            _unit = new Unit();
            _stock = new Stock();
            _purchaseItem = new PurchaseItem();
            _stock.AddStockItem(new StockItem(42m, _purchaseItem){Unit = _unit});
            _stockBooker = new StockBooker(new UnitConverter());
        };

        Because of = () => _foundItem = _stockBooker.BookItemOutOfStock(_stock, 0.42m, _unit, _purchaseItem);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);
        It should_have_right_quantity = () => _foundItem.Quantity.ShouldEqual(41.58m);

        static IStockBooker _stockBooker;
        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
        static Unit _unit;
    }

    [Subject(typeof(StockBooker))]
    public class When_booking_existing_stockitem_into_stock_with_different_units : WithUnitDepencies
    {
        Establish context = () =>
        {
            _stock = new Stock();
            _purchaseItem = new PurchaseItem();
            _stock.AddStockItem(new StockItem(42m, _purchaseItem){Unit = _unitKg});
            _stockBooker = new StockBooker(new UnitConverter());
        };

        Because of = () => _foundItem = _stockBooker.BookItemIntoStock(_stock, 42m, _unitG, _purchaseItem);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);
        It should_have_right_quantity = () => _foundItem.Quantity.ShouldEqual(42.042m);

        static IStockBooker _stockBooker;
        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
    }

    [Subject(typeof(StockBooker))]
    public class When_booking_existing_stockitem_out_of_stock_with_different_units : WithUnitDepencies
    {
        Establish context = () =>
        {
            _stock = new Stock();
            _purchaseItem = new PurchaseItem();
            _stock.AddStockItem(new StockItem(42m, _purchaseItem){Unit = _unitKg});
            _stockBooker = new StockBooker(new UnitConverter());
        };

        Because of = () => _foundItem = _stockBooker.BookItemOutOfStock(_stock, 42m, _unitG, _purchaseItem);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);
        It should_have_right_quantity = () => _foundItem.Quantity.ShouldEqual(41.958m);

        static IStockBooker _stockBooker;
        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
    }


    [Subject(typeof(StockBooker))]
    public class When_finding_existing_stockitem : WithInternalStockBooker
    {
        Establish context = () =>
        {
            _stock = new Stock();
            _purchaseItem = new PurchaseItem();
            _stock.AddStockItem(new StockItem(0m, _purchaseItem));
        };

        Because of = () => _foundItem = _stockBooker.FindStockItem(_stock, new Unit(), _purchaseItem, false);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);

        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
    }

    [Subject(typeof(StockBooker))]
    public class When_finding_non_existing_stockitem_without_creation_allowed : WithInternalStockBooker
    {
        Because of = () => _error = Catch.Exception(()=>_stockBooker.FindStockItem(new Stock(), new Unit(), new PurchaseItem(), false));

        It should_fail = () => _error.ShouldBeOfType<InvalidOperationException>();

        static Exception _error;
    }

    [Subject(typeof(StockBooker))]
    public class When_finding_non_existing_stockitem_with_creation_allowed : WithInternalStockBooker
    {
        Establish context = () =>
            {
                _stock = new Stock();
                _purchaseItem = new PurchaseItem();
                _unit = new Unit();
            };

        Because of = () => _foundItem = _stockBooker.FindStockItem(_stock, _unit, _purchaseItem, true);

        It should_find_item = () => _foundItem.RecipeableItem.ShouldEqual(_purchaseItem);
        It should_be_only_item_in_stock = () => _stock.StockItems.Count().ShouldEqual(1);
        It should_have_unit_set = () => _foundItem.Unit.ShouldEqual(_unit);
        It should_have_quantity_zero = () => _foundItem.Quantity.ShouldEqual(0m);

        static Stock _stock;
        static PurchaseItem _purchaseItem;
        static StockItem _foundItem;
        static Unit _unit;
    }


    [Subject(typeof(StockBooker))]
    public class WithInternalStockBooker
    {
        protected class InternalStockBooker : StockBooker
        {
            public InternalStockBooker(IUnitConverter unitConverter) : base(unitConverter)
            {
            }

            public new StockItem FindStockItem(Stock stock, Unit unit, RecipeableItem recipeableItem, bool createIfNotExists)
            {
                return base.FindStockItem(stock, unit, recipeableItem, createIfNotExists);
            }
        }

        Establish context = () =>
            {
                var unitConverter = MockRepository.GenerateMock<IUnitConverter>();
                _stockBooker = new InternalStockBooker(unitConverter);
            };

        protected static InternalStockBooker _stockBooker;
    }

    [Subject(typeof(StockBooker))]
    public class WithUnitDepencies
    {
        Establish context = () =>
            {
                _unitKg = new Unit {Contraction = "kg"};
                _unitG = new Unit {Contraction = "g", Parent = _unitKg, FactorToParent = 1000m};
            };

        protected static Unit _unitKg;
        protected static Unit _unitG;
    }
}