using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ums.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ums.Model.Queries
{
    public class AllUserRolesQuery : IDomainQuery<IEnumerable<UserRole>>
    {
        public IEnumerable<UserRole> Execute(ISession session)
        {
            return session.Query<UserRole>();
        }
    }
}