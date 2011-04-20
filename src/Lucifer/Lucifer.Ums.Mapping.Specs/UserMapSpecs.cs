using FluentNHibernate.Testing;
using Lucifer.Testing;
using Lucifer.Ums.Model.Entities;
using Machine.Specifications;

namespace Lucifer.Ums.Mapping.Specs
{
    [Subject(typeof(UserMap))]
    public class When_checking_persistence_specs_of_user : InMemoryDatabaseSpecs<UserMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<User>(Session);
            var userRole = new UserRole();
            spec.TransactionalSave(userRole);

            _check = spec
                .CheckProperty(c => c.Name, "A User")
                .CheckReference(c => c.UserRole, userRole)
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<User> _check;

    }
}
