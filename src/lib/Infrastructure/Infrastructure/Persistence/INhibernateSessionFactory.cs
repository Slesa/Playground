using System;
using NHibernate;

namespace Infrastructure.Persistence
{
    public interface INhibernateSessionFactory : IDisposable
    {
        ISession CreateSession();
    }
}