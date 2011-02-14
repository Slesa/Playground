using FluentNHibernate.Cfg.Db;

namespace Godot.Infrastructure.Configuration
{
    /// <summary>
    /// Liefert die Datenbank-Konfiguration von Fluent-NHibernate.
    /// </summary>
    public interface IPersistenceConfiguration
    {
        IPersistenceConfigurer GetConfiguration();
    }
}