using System;
using NHibernate;

namespace Lucifer.DataAccess.Persistence
{
    public interface INHibernateSessionFactory : IDisposable
    {
        ISession CreateSession();
    }
}