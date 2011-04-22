using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Pms.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Pms.Model.Queries
{
    public class AllDiscountsQuery : IDomainQuery<IEnumerable<Discount>>
    {
        public IEnumerable<Discount> Execute(ISession session)
        {
            return session.Query<Discount>();
        }
    }
}