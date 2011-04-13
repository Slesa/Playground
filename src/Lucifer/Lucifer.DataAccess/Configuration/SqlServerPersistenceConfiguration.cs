using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace Lucifer.DataAccess.Configuration
{
    public class SqlServerPersistenceConfiguration : IPersistenceConfiguration
    {
        readonly string _connectionString;
        public bool ShowSql { get; set; }

        public SqlServerPersistenceConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IPersistenceConfigurer GetConfiguration()
        {
            var configuration = MsSqlConfiguration
                .MsSql2005
                .ConnectionString(c => c.Is(_connectionString))
                .ProxyFactoryFactory(typeof(ProxyFactoryFactory).AssemblyQualifiedName)
                .AdoNetBatchSize(10)
                .UseReflectionOptimizer()
                .UseOuterJoin();

            if (ShowSql)
            {
                configuration.ShowSql();
            }

            return configuration;
        }
    }
}