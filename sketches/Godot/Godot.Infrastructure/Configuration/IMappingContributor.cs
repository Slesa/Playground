using FluentNHibernate.Cfg;

namespace Godot.Infrastructure.Configuration
{
    /// <summary>
    /// Ver�ndert die Mappings von Fluent-NHibernate auf irgendeine Weise.
    /// </summary>
    public interface IMappingContributor
    {
        void Apply(MappingConfiguration configuration);
    }
}