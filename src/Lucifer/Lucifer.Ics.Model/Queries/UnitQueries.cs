using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllUnitsQuery : IDomainQuery<IEnumerable<Unit>>
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Query<Unit>();
        }
    }
}