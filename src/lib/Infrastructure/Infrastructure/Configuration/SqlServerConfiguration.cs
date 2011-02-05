using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace Infrastructure.Configuration
{
    public class SqlServerConfiguration : IPersistenceConfiguration
    {
        readonly string _connectionString;

        public bool ShowSql { get; set; }

        public SqlServerConfiguration(IConfigurationReader configurationReader)
        {
            _connectionString = configurationReader.ValueOf("DbConnection");
            if (_connectionString == null)
                _connectionString = @"Server=.\SQLEXPRESS;initial catalog=Godot;Integrated Security=SSPI";
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