using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Pms.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Pms.Model.Queries
{
    public class AllPayformsQuery : IDomainQuery<IEnumerable<Payform>>
    {
        public IEnumerable<Payform> Execute(ISession session)
        {
            return session.Query<Payform>();
        }
    }
}