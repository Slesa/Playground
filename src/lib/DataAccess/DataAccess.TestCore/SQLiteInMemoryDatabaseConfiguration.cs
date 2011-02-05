using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace DataAccess.TestCore
{
    public class SqLiteInMemoryDatabaseConfiguration
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