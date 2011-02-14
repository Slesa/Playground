using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Godot.PmsModel.Entities;
using Godot.PmsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate.Tool.hbm2ddl;

namespace Godot.PmsNHibernate.MappingTests
{
    public class TestCore
    {
        Establish context
            = () =>
                {
                    _filename = Path.GetTempFileName();
                    Configuration =  Fluently.Configure()
                        .Database(SQLiteConfiguration.Standard.UsingFile(_filename)
                        /*              .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromAppSetting("DbConnection"))*/
                        /*.ShowSql()*/)
                        .Mappings(m => m.FluentMappings
                                           .AddFromAssemblyOf<SalesItem>().AddFromAssemblyOf<SalesItemMap>())
                        .ExposeConfiguration(config => new SchemaExport(config).Create(false, true));
                };

        protected static FluentConfiguration Configuration { get; private set; }

        Cleanup teardown = () => File.Delete(_filename);

        static string _filename;
    }
}