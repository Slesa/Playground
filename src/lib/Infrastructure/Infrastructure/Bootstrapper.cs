using System;
using System.IO;
using Castle.Core.Logging;
using Castle.Windsor;
using FluentNHibernate.Utils;

namespace Infrastructure
{
    public class Bootstrapper : IDisposable
    {
        public IWindsorContainer Container { get; private set; }

        protected Bootstrapper()
        {
            Container = ServiceLocation.Install();
        }

        public static Bootstrapper CreateBootstrapper()
        {
            var bootstrapper = new Bootstrapper();
            return bootstrapper.RunStartupConfiguration();
        }

        Bootstrapper RunStartupConfiguration()
        {
            var logger = Container.Resolve<ILogger>() ?? NullLogger.Instance;

            logger.InfoFormat("Starting up in {0}", Directory.GetCurrentDirectory());

            logger.InfoFormat("Registering components...");
            Container
                .ResolveAll<IRegisterComponentsOnStartup>()
                .Each(x => x.Configure());

            logger.InfoFormat("Configuring components...");
            Container
                .ResolveAll<IRequireConfigurationOnStartup>()
                .Each(x => x.Configure());

            logger.InfoFormat("Preparing startup...");
            Container
                .ResolveAll<IPrepareStartup>()
                .Each(x => x.Prepare());

            logger.InfoFormat("Startup complete");
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool @explicit)
        {
            if (!@explicit) return;
            if (Container == null) return;
            Container.Dispose();
            Container = null;
        }

        ~Bootstrapper()
        {
            Dispose(false);
        }
    }
}
