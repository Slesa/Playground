using FluentNHibernate.Cfg;

namespace Infrastructure.Configuration
{
    public interface IMappingContributor
    {
        void Apply(MappingConfiguration configuration);
    }
}
