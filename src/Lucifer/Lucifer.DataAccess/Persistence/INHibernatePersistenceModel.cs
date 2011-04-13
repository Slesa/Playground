using FluentNHibernate.Cfg;

namespace Lucifer.DataAccess.Persistence
{
    public interface INHibernatePersistenceModel
    {
        void AddMappings(MappingConfiguration configuration); 
    }
}