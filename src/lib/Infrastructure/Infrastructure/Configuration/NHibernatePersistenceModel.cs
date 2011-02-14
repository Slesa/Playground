using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;

namespace Infrastructure.Configuration
{
    public class NHibernatePersistenceModel : INHibernatePersistenceModel
    {
        public IMappingContributor[] MappingContributors
        {
            get;
            set;
        }

        public void AddMappings(MappingConfiguration configuration)
        {
            //if( MappingContributors!=null )
                MappingContributors.Each(x => x.Apply(configuration));
        }
    }
}