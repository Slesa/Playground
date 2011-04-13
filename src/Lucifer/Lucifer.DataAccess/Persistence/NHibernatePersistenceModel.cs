using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;

namespace Lucifer.DataAccess.Persistence
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
            MappingContributors.Each(x => x.Apply(configuration));
        }
       
    }
}