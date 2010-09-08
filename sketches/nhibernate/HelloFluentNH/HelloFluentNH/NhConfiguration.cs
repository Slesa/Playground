using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HelloFluentNH.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace HelloFluentNH
{
    public class NhConfiguration
    {
        private const string DatabaseFile = @".\HelloFluentNH.db";

        public static FluentConfiguration CreateConfiguration()
        {
            return Fluently.Configure()
//                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile))
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromAppSetting("DbConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Employee>());
        }

        public static ISessionFactory CreateSessionFactory()
        {
            if (!File.Exists(DatabaseFile))
                CreateDatabaseFile();
            return CreateConfiguration().BuildSessionFactory();
        }

        private static void CreateDatabaseFile()
        {
            var fluentConfiguration = CreateConfiguration();
            fluentConfiguration.ExposeConfiguration(BuildSchema).BuildSessionFactory();
        }

        private static void BuildSchema(Configuration conf)
        {
            new SchemaExport(conf).Drop(false, true);
            new SchemaExport(conf).Create(false, true);
        }
    }
}