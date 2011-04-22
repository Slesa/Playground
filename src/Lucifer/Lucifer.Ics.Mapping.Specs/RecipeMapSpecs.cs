using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;
using Machine.Specifications.Utility;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(RecipeMap))]
    public class When_checking_persistence_specs_of_recipe_map : InMemoryDatabaseSpecs<RecipeMap>
    {
        static PersistenceSpecification<Recipe> _check;

        Because of = () =>
        {
            var spec = new PersistenceSpecification<Recipe>(Session);

            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var unit = new Unit { UnitType = unitType };
            spec.TransactionalSave(unit);
            var productionItem = new ProductionItem { RecipeUnit = unit };
            spec.TransactionalSave(productionItem);

            var recipeItems = new[]
                    {
                        new RecipeItem(0.42m, productionItem), 
                        new RecipeItem(0.84m, productionItem), 
                    };

            _check = spec
                .CheckProperty(c => c.Plu, 42)
                //.CheckReference(c => c.SalesItem, salesItem)
                .CheckList(c => c.RecipeItems, recipeItems, (rec, items) => items.Each(rec.AddRecipeItem));
        };

        It should_be_verified = () => _check.VerifyTheMappings();

    }
}