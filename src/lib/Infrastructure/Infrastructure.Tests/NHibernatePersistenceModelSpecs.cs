using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using FluentNHibernate.Cfg;
using Infrastructure.Configuration;
using Infrastructure.Container;
using Machine.Specifications;

namespace Infrastructure.Tests
{
    // Internal, weil sonst mehrere MappingContributors gefunden werden
    [Subject(typeof(NHibernatePersistenceModel))]
    public class When_register_mapping_contributors
    {
        internal class MyMappingContributor1: IMappingContributor
        {
            public void Apply(MappingConfiguration configuration) { }
        }

        internal class MyMappingContributor2: IMappingContributor
        {
            public void Apply(MappingConfiguration configuration) { }
        }

        internal class MyMappingContributor3: IMappingContributor
        {
            public void Apply(MappingConfiguration configuration) { }
        }

        public class MappingContributorModule : IRegistrationContributor
        {
            public IEnumerable<IRegistration> GetRegistrations()
            {
                yield return Component
                    .For<IMappingContributor>()
                    .ImplementedBy<MyMappingContributor1>();
                yield return Component
                    .For<IMappingContributor>()
                    .ImplementedBy<MyMappingContributor2>();
                yield return Component
                    .For<IMappingContributor>()
                    .ImplementedBy<MyMappingContributor3>();
            }
        }

        Establish context = () =>
            {
                _bootstrapper = Bootstrapper.CreateBootstrapper();
            };

        Because of = () =>
            {
                _persistenceModel = (NHibernatePersistenceModel) _bootstrapper.Container.Resolve<INHibernatePersistenceModel>();
            };

        It should_have_four_mapping_contributors = () => _persistenceModel.MappingContributors.Count().ShouldEqual(4);

        It should_have_one_contributor1 = () =>
            _persistenceModel.MappingContributors.Where(x => x.GetType() == typeof (MyMappingContributor1)).Count().ShouldEqual(1);
        It should_have_one_contributor2 = () =>
            _persistenceModel.MappingContributors.Where(x => x.GetType() == typeof (MyMappingContributor2)).Count().ShouldEqual(1);
        It should_have_one_contributor3 = () =>
            _persistenceModel.MappingContributors.Where(x => x.GetType() == typeof (MyMappingContributor3)).Count().ShouldEqual(1);
        It should_have_one_fluentcontributor = () =>
            _persistenceModel.MappingContributors.Where(x => x.GetType() == typeof (FluentMappingConventions)).Count().ShouldEqual(1);

        static Bootstrapper _bootstrapper;
        static NHibernatePersistenceModel _persistenceModel;
    }
}