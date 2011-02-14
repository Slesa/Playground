using FluentNHibernate.Testing;
using Godot.PmsModel.Entities;
using Godot.PmsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.PmsNHibernate.MappingTests
{
    [Subject(typeof(CostcenterMap))]
    public class When_checking_persistence_specs_of_costcenter : TestCore
    {
        Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

        Because of = () =>
            {
                _check = new PersistenceSpecification<Costcenter>(_session)
                    .CheckProperty(c => c.Id, 1)
                    //.CheckProperty(c => c.MatrixId, 42)
                    .CheckProperty(c => c.Name, "Costcenter 42");
            };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Costcenter> _check;
        static ISession _session;
    }
}