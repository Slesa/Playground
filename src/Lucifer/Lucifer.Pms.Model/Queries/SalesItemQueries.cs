using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Pms.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Pms.Model.Queries
{
    public class AllSalesItemsQuery : IDomainQuery<IEnumerable<SalesItem>>
    {
        public IEnumerable<SalesItem> Execute(ISession session)
        {
            return session.Query<SalesItem>();
        }
    }
}