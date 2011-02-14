using System;
using Godot.Model;
using NHibernate;

namespace Godot.Infrastructure.Persistence
{
    class DbConversation : IDbConversation
    {
        readonly ISession _session;

        public DbConversation(INHibernateSessionFactory sessionFactory)
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

        public void Delete(object instance)
        {
            _session.Delete(instance);
        }

        public TResult GetById<TResult>(object key)
        {
            return _session.Load<TResult>(key);
        }

        public void UsingTransaction(Action action)
        {
            using (var transaction=_session.BeginTransaction())
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch 
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