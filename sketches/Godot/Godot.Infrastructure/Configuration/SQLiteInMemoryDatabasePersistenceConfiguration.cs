using FluentNHibernate.Cfg.Db;

using NHibernate.ByteCode.Castle;

namespace Godot.Infrastructure.Configuration
{
	public class SQLiteInMemoryDatabasePersistenceConfiguration : IPersistenceConfiguration
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