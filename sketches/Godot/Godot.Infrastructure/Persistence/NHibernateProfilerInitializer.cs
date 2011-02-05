using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;

namespace Godot.Infrastructure.Persistence
{
    public class NHibernateProfilerInitializer : INHibernateInitializationAware
    {
        public bool Enabled { get; set; }

#if DEBUG
        public NHibernateProfilerInitializer()
        {
            Enabled = true;
        }
#endif

        public void BeforeInitialization()
        {
        }

        public void Configuring(NHibernate.Cfg.Configuration configuration)
        {
        }

        public void Configured(NHibernate.Cfg.Configuration configuration)
        {
        }

        public void Initialized(NHibernate.Cfg.Configuration configuration, ISessionFactory sessionFactory)
        {
            if (!Enabled)
                return;
            NHibernateProfiler.Initialize();
        }
    }
}