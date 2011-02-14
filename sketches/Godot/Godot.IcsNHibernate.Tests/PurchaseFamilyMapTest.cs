using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(PurchaseFamilyMap))]
    public class PurchaseFamilyMapTest
    {
        public class When_checking_persistence_specs_of_purchase_family : TestCore
        {
            Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

            Because of = () =>
            {
                _check = new PersistenceSpecification<PurchaseFamily>(_session)
                    .CheckProperty(c => c.Name, "Purchase Family");
                _checkLength = new PersistenceSpecification<PurchaseFamily>(_session)
                    .CheckProperty(c => c.Name, new string('x', 60));
            };

            It should_be_verified = () => _check.VerifyTheMappings();
            It should_have_correct_name_length = () => _checkLength.VerifyTheMappings();

            static PersistenceSpecification<PurchaseFamily> _check;
            static PersistenceSpecification<PurchaseFamily> _checkLength;
            static ISession _session;
        }
    }
}