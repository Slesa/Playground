using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;

namespace Godot.Infrastructure.Configuration
{
    /// <summary>
    /// Sammelt die Änderungen der Mappings von Fluent-NHibernate in einer Liste, um sie der Konfiguration einzuimpfen.
    /// </summary>
    public class NHibernatePersistenceModel : INHibernatePersistenceModel
    {
        public IMappingContributor[] MappingContributors
        {
            get;
            set;
        }

        public void AddMappings(MappingConfiguration configuration)
        {
            MappingContributors.Each(x => x.Apply(configuration));
        }
    }
}