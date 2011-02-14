using System;
using System.Configuration;
using System.Windows;
using FluentNHibernate.Cfg;
using Godot.IcsEditor.Ui;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsEditor.Ui.ViewModel;
using Godot.Infrastructure;

namespace Godot.IcsEditor
{
    // Lokalisierung: 
    // http://wpflocalization.codeplex.com/
    // http://wpflocalization.codeplex.com/releases/view/29389

    // Prism: http://msdn.microsoft.com/de-de/magazine/cc785479.aspx
    // Mvvm: http://msdn.microsoft.com/de-de/magazine/dd419663.aspx

    // gg: Elfenlied
    // http://www.youtube.com/watch?v=h66bvTQTcBs&feature=related
    // http://www.larp-lieder.de/lied.php3?id=357
    // gg: IDomainQuery
    // http://elegantcode.com/2009/03/25/fubumvc-from-scratch-part-4-persistence/
    // http://www.lostechies.com/blogs/johnteague/archive/2010/01/30/implementing-domain-queries.aspx
    // http://www.thomasclaudiushuber.com/articles/200710_ModelViewViewModelArticle.pdf

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public Bootstrapper Bootstrapper { get; private set; }
        /*
                static string MatrixPath { get { return IcsEditor.Properties.Settings.Default.MatrixPath; } }
                static IRepository<SalesItem> _salesItemRepository;
                public static IRepository<SalesItem> SalesItemRepository
                {
                    get
                    {
                        if (_salesItemRepository == null)
                        {
                            var repository = new RepositoryMatrix<SalesItem> {DataPath = MatrixPath};
                            _salesItemRepository = repository;
                        }
                        return _salesItemRepository; // ?? (_salesItemRepository = Container.Resolve<IRepository<SalesItem>>());
                    }
                }
         */
        /*
                static void SettingChanging(object sender, SettingChangingEventArgs settingChangingEventArgs)
                {
                    if (settingChangingEventArgs.SettingName != "MatrixPath")
                        return;
                    var salesItems = _salesItemRepository as RepositoryMatrix<SalesItem>;
                    if (salesItems == null) return;
                    if (settingChangingEventArgs.NewValue.ToString() != salesItems.DataPath)
                        salesItems.Clear();
                }
        */

        protected override void OnStartup(StartupEventArgs e)
        {
            // Zum Debuggen und Profilen der Sql-Statements: (Reference nicht vergessen)
            // HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            // Zum Debuggen von Datenbindungsfehlern
            //PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            //PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Critical | SourceLevels.Error | SourceLevels.Warning; // | SourceLevels.All;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

            var splashscreen = new SplashScreen(@"Resources\Splashscreen.png");
            splashscreen.Show(true);
            var window = new MainWindow();

            try
            {
                Bootstrapper = Bootstrapper.CreateBootstrapper();
            }
            catch (FluentConfigurationException)
            {
                splashscreen.Close(new TimeSpan());
                MessageBox.Show("Fatal error: Database configuration mismatch.", "Configuration error", MessageBoxButton.OK);
                Shutdown(-1);
                return;
            }
            catch(Exception exception)
            {
                splashscreen.Close(new TimeSpan());
                MessageBox.Show(String.Format("Fatal error: unable to initialize environment.\n{0}", exception.Message)
                    , "Configuration error", MessageBoxButton.OK);
                Shutdown(-2);
                return;
            }


            //            IcsEditor.Properties.Settings.Default.SettingChanging += SettingChanging;


            // Create the ViewModel to which the main window binds.
            var viewModel = Bootstrapper.Container.Resolve<MainWindowViewModel>();

            // When the ViewModel asks to be closed,    
            // close the window.
            EventHandler handler = null;
            handler = delegate
                {
                    viewModel.RequestClose -= handler;
                    window.Close();
                };
            viewModel.RequestClose += handler;

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;
            window.Show();

            if (!EditSettings.ValidateMatrixPath(ConfigurationManager.AppSettings["MatrixPath"]))
                MessageBox.Show("Warning: path to matrix data is not valid! Please check your application settings.", "Configuration error", MessageBoxButton.OK);

        }

    }
}
