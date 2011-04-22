using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;
using Machine.Specifications.Utility;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(ProductionItemMap))]
    public class When_checking_persistence_specs_of_productionitem : InMemoryDatabaseSpecs<ProductionItemMap>
    {
        static PersistenceSpecification<ProductionItem> _check;

        Because of = () =>
        {
            var spec = new PersistenceSpecification<ProductionItem>(Session);
            var recipeUnitType = new UnitType();
            spec.TransactionalSave(recipeUnitType);
            var unit = new Unit { UnitType = recipeUnitType };
            spec.TransactionalSave(unit);
            var productionItem = new ProductionItem { RecipeUnit = unit };
            spec.TransactionalSave(productionItem);

            var recipeItems = new[]
                    {
                        new RecipeItem(0.42m, productionItem), 
                        new RecipeItem(0.84m, productionItem), 
                    };

            _check = spec
                .CheckProperty(c => c.Name, "Production Item")
                .CheckReference(c => c.RecipeUnit, unit)
                .CheckList(c => c.RecipeItems, recipeItems, (rec, items) => items.Each(rec.AddRecipeItem));
        };

        It should_be_verified = () => _check.VerifyTheMappings();
    }
}