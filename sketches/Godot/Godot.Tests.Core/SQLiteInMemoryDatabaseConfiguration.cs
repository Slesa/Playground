using FluentNHibernate.Cfg.Db;
using Godot.Infrastructure.Configuration;
using NHibernate.ByteCode.Castle;

namespace Godot.Tests.Core
{
    public class SQLiteInMemoryDatabaseConfiguration : IPersistenceConfiguration
    {
        public IPersistenceConfigurer GetConfiguration()
        {
            return SQLiteConfiguration
                .Standard
                .InMemory()
                .ShowSql()
                .ProxyFactoryFactory(typeof(ProxyFactoryFactory).AssemblyQualifiedName);
        }
    }
}