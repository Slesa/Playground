using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (ProductionItemMap))]
    public class When_checking_persistence_specs_of_production_item : TestCore
    {
        Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

        Because of = () =>
            {
                var spec = new PersistenceSpecification<ProductionItem>(_session);
                var family = new PurchaseFamily();
                spec.TransactionalSave(family);
                var purchaseUnit = new Unit();
                spec.TransactionalSave(purchaseUnit);
                var recipeUnit = new Unit();
                spec.TransactionalSave(recipeUnit);

                _check = spec
                    .CheckProperty(c => c.Name, "Production Item")
                    .CheckReference(c=>c.PurchaseFamily, family)
                    .CheckReference(c=>c.PurchaseUnit, purchaseUnit)
                    .CheckReference(c=>c.RecipeUnit, recipeUnit)
                    .CheckReference(c=>c.Recipe, new Recipe());
            };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<ProductionItem> _check;
        static ISession _session;
    }
}