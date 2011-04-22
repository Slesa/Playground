using System.Collections.Generic;
using System.Linq;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllProductionItemsQuery : IDomainQuery<IEnumerable<ProductionItem>>
    {
        public IEnumerable<ProductionItem> Execute(ISession session)
        {
            return session.Query<ProductionItem>();
        }
    }
}