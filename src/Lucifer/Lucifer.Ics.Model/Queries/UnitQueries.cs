using System.Collections.Generic;
using System.Linq;
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

    public class AllPurchaseUnitsQuery : IDomainQuery<IEnumerable<Unit>>
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return from x in session.Query<Unit>() where x.Purchasing select x;
        }
    }

    public class AllRecipeUnitsQuery : IDomainQuery<IEnumerable<Unit>>
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return from x in session.Query<Unit>() where x.Reciping select x;
        }
    }
}