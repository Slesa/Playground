using System.Windows;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new AppBootstrapper();
        }
    }
}