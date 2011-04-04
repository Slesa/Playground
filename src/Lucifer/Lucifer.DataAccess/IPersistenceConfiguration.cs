using FluentNHibernate.Cfg.Db;

namespace Lucifer.DataAccess
{
    public interface IPersistenceConfiguration
    {
        IPersistenceConfigurer GetConfiguration();
    }
}