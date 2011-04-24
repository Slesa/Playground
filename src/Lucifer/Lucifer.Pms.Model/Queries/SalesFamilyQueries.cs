using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Pms.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Pms.Model.Queries
{
    public class AllSalesFamiliesQuery : IDomainQuery<IEnumerable<SalesFamily>>
    {
        public IEnumerable<SalesFamily> Execute(ISession session)
        {
            return session.Query<SalesFamily>();
        }
    }
}