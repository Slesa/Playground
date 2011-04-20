using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllPurchaseFamiliesQuery : IDomainQuery<IEnumerable<PurchaseFamily>>
    {
        public IEnumerable<PurchaseFamily> Execute(ISession session)
        {
            return session.Query<PurchaseFamily>();
        }
    }
}