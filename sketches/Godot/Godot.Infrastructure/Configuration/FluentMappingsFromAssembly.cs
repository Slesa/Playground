using System.Reflection;

using FluentNHibernate.Cfg;

namespace Godot.Infrastructure.Configuration
{
    public class FluentMappingsFromAssembly : IMappingContributor
    {
        readonly Assembly _assembly;

        public FluentMappingsFromAssembly(string assembly)
        {
            _assembly = Assembly.LoadFrom(assembly);
        }

        public void Apply(MappingConfiguration configuration)
        {
            configuration.FluentMappings.AddFromAssembly(_assembly);
        }
    }
}