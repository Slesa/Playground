using System;
using DataAccess;
using NHibernate;

namespace Infrastructure.Persistence
{
    class DbConversation : IDbConversation
    {
        readonly ISession _session;

        public DbConversation(INhibernateSessionFactory sessionFactory)
        {
            _session = sessionFactory.CreateSession();
        }

        public TResult Query<TResult>(IDomainQuery<TResult> query)
        {
            return query.Execute(_session);
        }

        public void InsertObjectOnCommit(object instance)
        {
            _session.SaveOrUpdate(instance);
        }

        public TResult GetById<TResult>(object key)
        {
            return _session.Load<TResult>(key);
        }

        public void Delete(object instance)
        {
            _session.Delete(instance);
        }

        public void UsingTransaction(Action action)
        {
            using(var transaction=_session.BeginTransaction())
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            if( _session!=null )
                _session.Dispose();
        }

    }
}
