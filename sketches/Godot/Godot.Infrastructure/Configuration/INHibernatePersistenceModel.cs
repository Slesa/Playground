using FluentNHibernate.Cfg;

namespace Godot.Infrastructure.Configuration
{
    /// <summary>
    /// Interface um die Mapping-Konfiguration von Fluent-NHibernate als solche zu ermitteln.
    /// </summary>
    public interface INHibernatePersistenceModel
    {
        void AddMappings(MappingConfiguration configuration);
    }
}