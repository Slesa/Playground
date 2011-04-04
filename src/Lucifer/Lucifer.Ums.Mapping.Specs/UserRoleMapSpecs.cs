using FluentNHibernate.Testing;
using Lucifer.Testing;
using Lucifer.Ums.Model.Entities;
using Machine.Specifications;

namespace Lucifer.Ums.Mapping.Specs
{
    [Subject(typeof(UserRoleMap))]
    public class When_checking_persistence_specs_of_user_role : InMemoryDatabaseSpecs<UserRoleMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<UserRole>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A User Role")
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<UserRole> _check;

    }
}