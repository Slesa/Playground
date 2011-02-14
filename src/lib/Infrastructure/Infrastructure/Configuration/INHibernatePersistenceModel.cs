using FluentNHibernate.Cfg;

namespace Infrastructure.Configuration
{
    public interface INHibernatePersistenceModel
    {
        void AddMappings(MappingConfiguration configuration);
    }
}