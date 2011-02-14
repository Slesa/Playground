using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using FluentNHibernate.Testing;
using Godot.Model;
using Godot.PmsMatrix.Mappings;
using Godot.PmsMatrix.Persistence;
using Godot.PmsModel.Entities;
using Godot.Tests.Core;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Godot.Infrastructure.Container;

namespace Godot.PmsMatrix.Tests
{
    public class FromDatabase : DomainEntity
    {
        public virtual string DisplayName { get; set; }
        public virtual SalesItem SalesItem { get; set; }
        public virtual IList<SalesItem> Collection { get; set; }
    }

    public class FromDatabaseMap : ClassMap<FromDatabase>
    {
        public FromDatabaseMap()
        {
            Id(x => x.Id)
                .GeneratedBy.HiLo("10");
            //.Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore);

            Map(x => x.DisplayName);
            //.Access.BackingField();

            References(x => x.SalesItem)
                //.Access.BackingField()
                .Cascade.All()
                .Not.Nullable();

            HasMany(x => x.Collection)
                //.Access.BackingField()
                .Cascade.AllDeleteOrphan()
                .AsBag();
        }
    }

    [Subject(typeof (SalesItemMap))]
    public class When_referencing_salesitem_from_database
    {
        static SalesItem _salesItem;
        static IEnumerable<SalesItem> _collection;
        static PersistenceSpecification<FromDatabase> _check;

        Establish context
            = () =>
                {
                    NHibernateProfiler.Initialize();

                    // Force SQLite assembly in output path.
                    // ReSharper disable ConditionIsAlwaysTrueOrFalse
                    var forceSqlLiteReference = typeof(SQLiteException) != null;
                    Trace.Assert(forceSqlLiteReference);
                    Debug.Assert(forceSqlLiteReference);
                    // ReSharper restore ConditionIsAlwaysTrueOrFalse

                    var configuration = Fluently.Configure()
                        .Database(new SQLiteInMemoryDatabaseConfiguration().GetConfiguration())
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<FromDatabaseMap>())
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<SalesItemMap>());
                    //                        .ExposeConfiguration(config => new SchemaExport(config).Create(false, true));

                    var source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
                    source.BuildSchema();

                    SessionFactory = source.SessionFactory;
                    Session = source.CreateSession();

                    var container = new WindsorContainer();
                    ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
                    container.Register(Component
                                           .For<IMatrixFileLoader<SalesItem>>()
                                           .Instance(new DatFileLoader<SalesItem>(new DatFileMapper())));

                    _salesItem = new SalesItem {/*Plu = 42,*/ Name = "SaleItem"}.SetInternalId(42);
                    _collection = new[]
                        {new SalesItem {/*Plu = 1,*/ Name = "Article 1"}.SetInternalId(1), new SalesItem {/*Plu = 2,*/ Name = "Article 2"}.SetInternalId(2)};
                };

        Cleanup after = () =>
            {
                if (Session != null) Session.Close();
                NHibernateProfiler.Stop();
            };

        Because of = () =>
            {
                var spec = new PersistenceSpecification<FromDatabase>(Session);
                _check = spec
                    .CheckProperty(x => x.DisplayName, "The display name")
                    .CheckReference(x => x.SalesItem, _salesItem)
                    .CheckList(x => x.Collection, _collection);
            };

        It should_be_able_to_store_and_retrieve_objects_correctly = () => _check.VerifyTheMappings();

        static ISessionFactory SessionFactory { get; set; }
        static ISession Session { get; set; }
    }

    [Subject(typeof (SalesItemMap))]
    public class When_checking_persistence_specs_of_salesitem : InMemoryDatabaseSpecs<SalesItemMap>
    {
        static PersistenceSpecification<SalesItem> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<SalesItem>(Session);

                _check = spec
                    //.CheckProperty(c => c.Plu, 42)
                    .CheckProperty(c => c.Name, "SalesItem");
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }
}