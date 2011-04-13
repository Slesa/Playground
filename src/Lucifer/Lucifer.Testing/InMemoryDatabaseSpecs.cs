using System.Data.SQLite;
using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Testing;
using Machine.Specifications;
using NHibernate;

namespace Lucifer.Testing
{
    public class InMemoryDatabaseSpecs<TMappingAssembly>
    {
        Establish context
            = () =>
            {
                //NHibernateProfiler.Initialize();

                // Force SQLite assembly in output path.
                // ReSharper disable ConditionIsAlwaysTrueOrFalse
                var forceSqlLiteReference = typeof(SQLiteException) != null;
                Trace.Assert(forceSqlLiteReference);
                Debug.Assert(forceSqlLiteReference);
                // ReSharper restore ConditionIsAlwaysTrueOrFalse

                var configuration = Fluently.Configure()
                    .Database(new SqLiteInMemoryDatabaseConfiguration().GetConfiguration())
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TMappingAssembly>());
                //                        .ExposeConfiguration(config => new SchemaExport(config).Create(false, true));

                var source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
                source.BuildSchema();

                SessionFactory = source.SessionFactory;
                Session = source.CreateSession();

            };

        Cleanup after = () =>
        {
            if (Session != null) Session.Close();
            //NHibernateProfiler.Stop();
        };

        protected static ISessionFactory SessionFactory { get; private set; }
        protected static ISession Session { get; set; }
    }
}
