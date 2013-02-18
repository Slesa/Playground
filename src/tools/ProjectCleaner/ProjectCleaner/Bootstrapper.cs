using System.IO;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ProjectCleaner.Modules;
using ProjectCleaner.ViewModels;
using ProjectCleaner.Views;

namespace ProjectCleaner
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            log4net.Config.XmlConfigurator.Configure(new FileInfo("ProjectCleaner.log4net.config"));

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType(typeof (ShellViewModel), typeof (ShellViewModel));

            Container.RegisterType<IProcessProjects, BackupProjectFileProcessor>("backup");
            Container.RegisterType<IProcessProjects, RemoveSourceControlProcessor>("sourcecontrol");
            Container.RegisterType<IProcessProjects, RemoveUnknownTargetProcessor>("unknowntargets");
            Container.RegisterType<IProcessProjects, ResetTargetFrameworkProcessor>("targetframework");

            Container.RegisterType<ICollectProjects, ProjectCollector>();
            Container.RegisterType<CleanupProcessor, CleanupProcessor>();
        }
    }
}