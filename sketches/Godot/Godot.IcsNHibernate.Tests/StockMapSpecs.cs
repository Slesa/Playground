using System;
using FluentNHibernate.Testing;
using FluentNHibernate.Utils;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(StockMap))]
    public class When_checking_persistence_specs_of_stock_map : InMemoryDatabaseSpecs<StockMap>
    {
        static PersistenceSpecification<Stock> _check;

        Because of = () =>
        {
            var spec = new PersistenceSpecification<StockItem>(Session);

            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var unit = new Unit { UnitType = unitType };
            spec.TransactionalSave(unit);
            var productionItem = new ProductionItem { RecipeUnit = unit };
            spec.TransactionalSave(productionItem);

            var stockItems = new[]
                {
                    new StockItem(0.42m, productionItem) , 
                    new StockItem(0.84m, productionItem) ,
                };

            _check = new PersistenceSpecification<Stock>(Session)
                .CheckProperty(c => c.Name, "Stock One")
                .CheckList(c=>c.StockItems, stockItems, (st, items) => items.Each(st.AddStockItem));
        };

        It should_be_verified = () => _check.VerifyTheMappings();

    }

    [Subject(typeof(StockMap))]
    public class When_editing_stock_in_background : InFileDatabaseSpecs<StockMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
        {
            var session = SessionFactory.OpenSession();
            var stock = new Stock { Name = "Stock" };
            using (var transaction = session.BeginTransaction())
            {
                session.Save(stock);
                transaction.Commit();
            }
            _id = stock.Id;
            session.Close();
        };

        Because of = () =>
        {
            var sessionForeground = SessionFactory.OpenSession();
            var stockForeground = sessionForeground.Load<Stock>(_id);
            stockForeground.Name.ShouldEqual("Stock");

            var sessionBackground = SessionFactory.OpenSession();
            var stockBackground = sessionBackground.Load<Stock>(_id);

            using (var transaction = sessionBackground.BeginTransaction())
            {
                stockBackground.Name = "Background";
                sessionBackground.SaveOrUpdate(stockBackground);
                transaction.Commit();
            }
            sessionBackground.Close();

            using (var transaction = sessionForeground.BeginTransaction())
            {
                stockForeground.Name = "Foreground";
                sessionForeground.SaveOrUpdate(stockForeground);
                _exception = Catch.Exception(transaction.Commit);
            }
            sessionForeground.Close();

            using (var session = SessionFactory.OpenSession())
            {
                var stock = session.Load<Stock>(_id);
                _name = stock.Name;
            }
        };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof(StaleObjectStateException));
    }

}