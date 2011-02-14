using System;
using FluentNHibernate.Testing;
using FluentNHibernate.Utils;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;
using NHibernate.Cfg;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (RecipeMap))]
    public class When_checking_persistence_specs_of_recipe_map : InMemoryDatabaseSpecs<RecipeMap>
    {
        static PersistenceSpecification<Recipe> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<Recipe>(Session);

                var unitType = new UnitType();
                spec.TransactionalSave(unitType);
                var unit = new Unit {UnitType = unitType};
                spec.TransactionalSave(unit);
                var productionItem = new ProductionItem{RecipeUnit = unit};
                spec.TransactionalSave(productionItem);

                var recipeItems = new[]
                    {
                        new RecipeItem(0.42m, productionItem), 
                        new RecipeItem(0.84m, productionItem), 
                    };
                
                _check = spec
                    .CheckProperty(c => c.Plu, 42)
                    //.CheckReference(c => c.SalesItem, salesItem)
                    .CheckList(c=>c.RecipeItems, recipeItems, (rec, items) => items.Each(rec.AddRecipeItem));
            };

        It should_be_verified = () => _check.VerifyTheMappings();

    }

    [Subject(typeof(RecipeMap))]
    public class When_editing_recipe_in_background : InFileDatabaseSpecs<StockMap>
    {
        static int _id;
        static int _plu;
        static Exception _exception;

        Establish context = () =>
        {
            var session = SessionFactory.OpenSession();
            var recipe = new Recipe { Plu = 42 };
            using (var transaction = session.BeginTransaction())
            {
                session.Save(recipe);
                transaction.Commit();
            }
            _id = recipe.Id;
            session.Close();
        };

        Because of = () =>
        {
            var sessionForeground = SessionFactory.OpenSession();
            var recipeForeground = sessionForeground.Load<Recipe>(_id);
            recipeForeground.Plu.ShouldEqual(42);

            var sessionBackground = SessionFactory.OpenSession();
            var recipeBackground = sessionBackground.Load<Recipe>(_id);

            using (var transaction = sessionBackground.BeginTransaction())
            {
                recipeBackground.Plu = 43;
                sessionBackground.SaveOrUpdate(recipeBackground);
                transaction.Commit();
            }
            sessionBackground.Close();

            using (var transaction = sessionForeground.BeginTransaction())
            {
                recipeForeground.Plu = 1;
                sessionForeground.SaveOrUpdate(recipeForeground);
                _exception = Catch.Exception(transaction.Commit);
            }
            sessionForeground.Close();

            using (var session = SessionFactory.OpenSession())
            {
                var recipe = session.Load<Recipe>(_id);
                _plu = recipe.Plu;
            }
        };

        It should_have_the_right_plu = () => _plu.ShouldEqual(43);
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof(StaleObjectStateException));
    }

}