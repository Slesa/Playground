using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllUnitTypesQuery : IDomainQuery<IEnumerable<UnitType>>
    {
        public IEnumerable<UnitType> Execute(ISession session)
        {
            return session.Query<UnitType>();
        }
    }
}