using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllPurchaseItemsQuery : IDomainQuery<IEnumerable<PurchaseItem>>
    {
        public IEnumerable<PurchaseItem> Execute(ISession session)
        {
            return session.Query<PurchaseItem>();
        }
    }
}