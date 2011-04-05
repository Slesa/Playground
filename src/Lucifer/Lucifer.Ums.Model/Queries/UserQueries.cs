using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ums.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ums.Model.Queries
{
    public class AllUsersQuery : IDomainQuery<IEnumerable<User>>
    {
        public IEnumerable<User> Execute(ISession session)
        {
            return session.Query<User>();
        }
    }
}