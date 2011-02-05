using System;
using System.IO;
using Castle.Core.Logging;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using Godot.Infrastructure.Container;

namespace Godot.Infrastructure
{
    // http://using.castleproject.org/display/IoC/Fluent+Registration+API
    // http://www.ayende.com/presentations.aspx
    // Service Locator: http://msdn.microsoft.com/en-us/library/ff649658.aspx
    // Event aggregator: http://martinfowler.com/eaaDev/EventAggregator.html
    // Value types: http://moneytype.codeplex.com/

    public class Bootstrapper : IDisposable
    {
        public IWindsorContainer Container { get; private set; }

        static ILogger Logger;
        
        protected Bootstrapper()
        {
            Container = ServiceLocation.Install();
            Logger = Container.Resolve<ILogger>() ?? NullLogger.Instance;
        }

        public static Bootstrapper CreateBootstrapper()
        {
            try
            {
                var bootstrapper = new Bootstrapper();
                Logger.Info("Creating startup configuration");
                var result = bootstrapper.RunStartupConfiguration();
                Logger.Info("Startup configuration finished");
                return result;
            }
            catch (FluentConfigurationException exception)
            {
                Logger.Fatal("Fatal error: Database configuration mismatch", exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Fatal("Fatal error: unable to initialize environment", exception);
                throw;
            }
            catch
            {
                Logger.Fatal("Fatal error: does not know whats going on");
                throw;
            }
        }

        Bootstrapper RunStartupConfiguration()
        {
            Logger.InfoFormat("Starting up in {0}", Directory.GetCurrentDirectory());

            Logger.Info("Registering components...");
            Container
                .ResolveAll<IRegisterComponentsOnStartup>()
                .Each(x => x.Configure());

            Logger.Info("Configuring components...");
            Container
                .ResolveAll<IRequireConfigurationOnStartup>()
                .Each(x => x.Configure());

            Logger.Info("Preparing startup...");
            Container
                .ResolveAll<IPrepareStartup>()
                .Each(x => x.Prepare());

            Logger.Info("Startup complete");
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            // Prevent the destructor from being called.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool @explicit)
        {
            // If explicit is true, then this method was called through
            // the public Dispose().
            if (@explicit)
            {
                // Release or cleanup managed resources.
                if (Container != null)
                {
                    Container.Dispose();
                    Container = null;
                }
            }
            // Always release or cleanup (any) unmanaged resources.
        }

        ~Bootstrapper()
        {
            // Since other managed ob jects are disposed automatically,
            // we should not try to dispose any managed resources.
            // We therefore pass false to Dispose().
            Dispose(false);
        }
    }
}