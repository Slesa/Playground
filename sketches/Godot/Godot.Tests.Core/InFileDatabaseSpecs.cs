using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Machine.Specifications;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Godot.Tests.Core
{
    [Subject(typeof(InFileDatabaseSpecs<>))]
    public class InFileDatabaseSpecs<TMappingAssembly>
    {
        static string _filename;

        Establish context
            = () =>
              {
                  NHibernateProfiler.Initialize();

                  _filename = typeof(TMappingAssembly).Name + ".dbs";

                  // Force SQLite assembly in output path.
                  // ReSharper disable ConditionIsAlwaysTrueOrFalse
                  bool forceSqlLiteReference = typeof(SQLiteException) != null;
                  Trace.Assert(forceSqlLiteReference);
                  Debug.Assert(forceSqlLiteReference);
                  // ReSharper restore ConditionIsAlwaysTrueOrFalse

                  var configuration = Fluently.Configure()
                      .Database(SQLiteConfiguration.Standard.UsingFile(_filename))
                      .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TMappingAssembly>())
                      .ExposeConfiguration(config => new SchemaExport(config).Create(false, true));

                  SessionFactory = configuration.BuildSessionFactory();
                  Session = SessionFactory.OpenSession();

              };

        Cleanup after = () =>
                        {
                            if (Session != null) Session.Close();
                            File.Delete(_filename);
                            NHibernateProfiler.Stop();
                        };

        protected static ISessionFactory SessionFactory { get; private set; }
        protected static ISession Session { get; set; }

    }
}