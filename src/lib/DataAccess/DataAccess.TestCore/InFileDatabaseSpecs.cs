using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Machine.Specifications;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DataAccess.TestCore
{
    // http://nuget.codeplex.com/workitem/215

    [Subject(typeof(InFileDatabaseSpecs<>))]
    public class InFileDatabaseSpecs<TMappingAssembly>
    {
        static string _filename;

        Establish context
            = () =>
            {
                NHibernateProfiler.Initialize();

                _filename = typeof(TMappingAssembly).Name + DateTime.Now.Millisecond + ".dbs";

                // Force SQLite assembly in output path.
                // ReSharper disable ConditionIsAlwaysTrueOrFalse
                var forceSqlLiteReference = typeof(SQLiteException) != null;
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
            if( File.Exists(_filename) )
                File.Delete(_filename);
            NHibernateProfiler.Stop();
        };

        protected static ISessionFactory SessionFactory { get; private set; }
        protected static ISession Session { get; set; }

        #region ConcurrencyChecks

        public static int SaveEntityAndGetItsId<TEntity>(string fieldName="Name") where TEntity : DomainEntity, new()
        {
            var type = typeof(TEntity);
            var property = type.GetProperty(fieldName);
            var saveString = property.PropertyType == typeof(string);

            var entity = new TEntity();
            if( saveString )
                property.SetValue(entity, type.Name, null);
            else
                property.SetValue(entity, 42, null);

            var session = SessionFactory.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Save(entity);
                transaction.Commit();
            }
            var id = entity.Id;
            session.Close();
            return id;
        }

        const int Backid = 43;
        const int Foreid = 1;
        const string Background = "Background";
        const string Foreground = "Foreground";

        public static void SaveEntityTwice<TEntity>(int id, string fieldName="Name") where TEntity : DomainEntity
        {
            var type = typeof (TEntity);
            var property = type.GetProperty(fieldName);
            var saveString = property.PropertyType == typeof (string);

            var foregroundSession = SessionFactory.OpenSession();
            var foregroundEntity = foregroundSession.Load<TEntity>(id);
            if( saveString )
                property.GetValue(foregroundEntity, null).ShouldEqual(type.Name);

            var backgroundSession = SessionFactory.OpenSession();
            var backgroundEntity = backgroundSession.Load<TEntity>(id);
            if (saveString)
                property.GetValue(backgroundEntity, null).ShouldEqual(type.Name);

            using (var transaction = backgroundSession.BeginTransaction())
            {
                if( saveString )
                    property.SetValue(backgroundEntity, Background, null);
                else
                    property.SetValue(backgroundEntity, Backid, null);
                backgroundSession.SaveOrUpdate(backgroundEntity);
                transaction.Commit();
            }
            backgroundSession.Close();

            using (var transaction = foregroundSession.BeginTransaction())
            {
                if( saveString )
                    property.SetValue(foregroundEntity, Foreground, null);
                else
                    property.SetValue(foregroundEntity, Foreid, null);
                foregroundSession.SaveOrUpdate(foregroundEntity);
                transaction.Commit();
            }
            foregroundSession.Close();

            using( var session = SessionFactory.OpenSession() )
            {
                var entity = session.Load<TEntity>(id);
                if( saveString)
                    property.GetValue(entity, null).ShouldEqual(Background);
            }
        }

        #endregion

    }
}