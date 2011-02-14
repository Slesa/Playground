using Godot.Infrastructure;
using Godot.Infrastructure.Persistence;

namespace Godot.IcsEditor.Ui.Infrastructure
{
    public class ConnectDatabaseOnStartup : IRequireConfigurationOnStartup
    {
        readonly INHibernateSessionFactory _sessionFactory;

        public ConnectDatabaseOnStartup(INHibernateSessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Configure()
        {
            var sessionFactory = _sessionFactory as NHibernateSessionFactory;
            if( sessionFactory!=null )
                sessionFactory.Configure();
        }
    }
}