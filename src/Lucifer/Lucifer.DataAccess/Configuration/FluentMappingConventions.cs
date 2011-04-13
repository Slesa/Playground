using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Lucifer.DataAccess.Persistence;

namespace Lucifer.DataAccess.Configuration
{
    public class FluentMappingConventions : IMappingContributor
    {
        public void Apply(MappingConfiguration configuration)
        {
            var conventions = configuration.FluentMappings.Conventions;

            conventions.Add(ConventionBuilder
                                .Class
                                .Always(x => x.Table(Inflector.Net.Inflector.Pluralize(x.EntityType.Name).Escape())));
  
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