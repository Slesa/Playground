using FluentNHibernate.Testing;
using Lucifer.Testing;
using Lucifer.Ums.Model.Entities;
using Machine.Specifications;

namespace Lucifer.Ums.Mapping.Specs
{
    [Subject(typeof(UserRoleItemMap))]
    public class When_checking_persistence_specs_of_user_role_item : InMemoryDatabaseSpecs<UserRoleMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<UserRoleItem>(Session);
            _check = spec
                .CheckProperty(c => c.Program, "A program")
                .CheckProperty(c => c.Module, "A module")
                .CheckProperty(c => c.Function, "A function")
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<UserRoleItem> _check;

    }
}