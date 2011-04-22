using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllRecipesQuery : IDomainQuery<IEnumerable<Recipe>>
    {
        public IEnumerable<Recipe> Execute(ISession session)
        {
            return session.Query<Recipe>();
        }
    }
}