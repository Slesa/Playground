using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(RecipeMapperMap))]
    public class When_checking_persistence_specs_of_recipe_mapper_map : InMemoryDatabaseSpecs
    {
        Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

        Because of = () =>
            {
                _check = new PersistenceSpecification<RecipeMapper>(_session)
                    .CheckProperty(c => c.SalesItem, 42)
                    .CheckProperty(c => c.Costcenter, 43)
                    .CheckReference(c => c.Recipe, new Recipe());
            };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<RecipeMapper> _check;
        static ISession _session;
    }
}