using System.Diagnostics;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Lucifer.DataAccess.Persistence;
using NHibernate;

namespace Lucifer.DataAccess.Configuration
{
    public class NhProfilerInitializer : INHibernateInitializationAware
    {
         public bool Enabled { get; set; }

        public NhProfilerInitializer()
        {
            EnableProfiler();
        }

        [Conditional("DEBUG")]
        void EnableProfiler()
        {
            Enabled = true;
        }

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