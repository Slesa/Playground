using FluentNHibernate.Testing;
using Godot.PmsModel.Entities;
using Godot.PmsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.PmsNHibernate.MappingTests
{
    [Subject(typeof (SalesItemMap))]
    public class When_checking_persistence_specs_of_sales_item : TestCore
    {
        Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

        Because of = () =>
            {
                _check = new PersistenceSpecification<SalesItem>(_session)
                    .CheckProperty(c => c.Id, 1)
                    //.CheckProperty(c => c.Plu, 42)
                    .CheckProperty(c=>c.Name, "Article 42");
            };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<SalesItem> _check;
        static ISession _session;
    }
}