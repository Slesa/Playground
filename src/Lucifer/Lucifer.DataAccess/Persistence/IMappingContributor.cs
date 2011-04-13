using FluentNHibernate.Cfg;

namespace Lucifer.DataAccess.Persistence
{
    public interface IMappingContributor
    {
        void Apply(MappingConfiguration configuration);
    }
}