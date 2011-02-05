using System;

using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;

namespace Godot.Infrastructure.Configuration
{
    public class FluentMappingConventions : IMappingContributor
    {
        /// <summary>
        /// Vordefinieren von Konventionen für die Mappings.
        /// </summary>
        public void Apply(MappingConfiguration configuration)
        {
            var conventions = configuration.FluentMappings.Conventions;

            conventions.Add(ConventionBuilder
                                .Class
                                .Always(x => x.Table(Inflector.Net.Inflector.Pluralize(x.EntityType.Name).Escape())));

            /* Kann man machen, muss man aber nicht...
            conventions.Add(ConventionBuilder
                                .Id
                                .Always(x => x.Column(String.Format("{0}ID", x.EntityType.Name))));

            conventions.Add(ConventionBuilder.Reference.Always(x =>
                {
                    x.Column(String.Format("{0}ID", x.Property.Name));
                    x.ForeignKey(String.Format("FK_{0}To{1}", x.EntityType.Name, x.Property.Name));
                }));

            conventions.Add(ConventionBuilder.HasMany.Always(x =>
                {
                    x.Key.Column(String.Format("{0}ID", x.Key.EntityType.Name));
                    x.Key.ForeignKey(String.Format("FK_{0}To{1}",
                                                   x.Relationship.StringIdentifierForModel,
                                                   x.Relationship.EntityType.Name));
                }));
            */

            conventions.Add(ConventionBuilder.Property.Always(x => x.Column(x.Property.Name)));

            conventions.Add(DefaultLazy.Always());
        }
    }

    static class StringExtensions
    {
        public static string Escape(this string instance)
        {
            return String.Format("`{0}`", instance);
        }
    }
}