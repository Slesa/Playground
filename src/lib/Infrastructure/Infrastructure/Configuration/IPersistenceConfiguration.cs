using FluentNHibernate.Cfg.Db;

namespace Infrastructure.Configuration
{
    public interface IPersistenceConfiguration
    {
        IPersistenceConfigurer GetConfiguration();
    }
}