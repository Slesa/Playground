using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Pms.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Pms.Model.Queries
{
    public class AllCurrenciesQuery : IDomainQuery<IEnumerable<Currency>>
    {
        public IEnumerable<Currency> Execute(ISession session)
        {
            return session.Query<Currency>();
        }
    }
}