using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace Infrastructure.Configuration
{
    public class SqLiteInMemoryConfiguration : IPersistenceConfiguration
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