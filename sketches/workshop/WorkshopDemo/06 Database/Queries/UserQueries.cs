using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models;
using NHibernate;
using NHibernate.Linq;

namespace Database.Queries
{
    public class AllUsersQuery : IDomainQuery<IEnumerable<User>>
    {
        public IEnumerable<User> Execute(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");
            return session.Query<User>();
        }
    }

    public class UserByNameQuery : IDomainQuery<User>
    {
        readonly string _userName;

        public UserByNameQuery(string userName)
        {
            _userName = userName;
        }

        public User Execute(ISession session)
        {
            return session.Query<User>().FirstOrDefault(x => x.Name.Equals(_userName));
        }
    }
}