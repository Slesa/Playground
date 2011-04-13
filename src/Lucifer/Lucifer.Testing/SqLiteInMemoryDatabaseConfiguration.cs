using FluentNHibernate.Cfg.Db;
using Lucifer.DataAccess;
using NHibernate.ByteCode.Castle;

namespace Lucifer.Testing
{
    public class SqLiteInMemoryDatabaseConfiguration : IPersistenceConfiguration
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