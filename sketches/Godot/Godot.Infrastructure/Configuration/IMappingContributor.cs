using FluentNHibernate.Cfg;

namespace Godot.Infrastructure.Configuration
{
    /// <summary>
    /// Verändert die Mappings von Fluent-NHibernate auf irgendeine Weise.
    /// </summary>
    public interface IMappingContributor
    {
        void Apply(MappingConfiguration configuration);
    }
}