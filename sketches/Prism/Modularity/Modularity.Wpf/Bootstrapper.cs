using System;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Modules.Wpf;
using UnityContainerExtensions = Microsoft.Practices.Unity.UnityContainerExtensions;

namespace Modularity.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        readonly CallbackLogger _callbackLogger = new CallbackLogger();

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

#if SILVERLIGHT
            Application.Current.RootVisual = (UIElement) Shell;
#else
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
#endif
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _callbackLogger;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof (IModuleTracker), typeof (ModuleTracker), true);
            UnityContainerExtensions.RegisterInstance<CallbackLogger>(Container, _callbackLogger);
            //Container.RegisterInstance<CallbackLogger>(_callbackLogger);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
#if SILVERLIGHT
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/Modularity.Sl;component/ModulesCatalog.xaml", UriKind.Relative));
#else
            return new AggregateModuleCatalog();
#endif
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleAType = typeof (ModuleA.Wpf.ModuleA);
            ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));

            var moduleCType = typeof (ModuleC.Wpf.ModuleC);
            ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = moduleCType.Name,
                    ModuleType = moduleCType.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.OnDemand
                });
#if (!SILVERLIGHT)
            var directoryCatalog = new DirectoryModuleCatalog {ModulePath = @".\Modules"};
            ((AggregateModuleCatalog) ModuleCatalog).AddCatalog(directoryCatalog);

            var configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog) ModuleCatalog).AddCatalog(configurationCatalog);
#endif
        }
    }
}