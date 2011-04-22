using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllStocksQuery : IDomainQuery<IEnumerable<Stock>>
    {
        public IEnumerable<Stock> Execute(ISession session)
        {
            return session.Query<Stock>();
        }
    }
}