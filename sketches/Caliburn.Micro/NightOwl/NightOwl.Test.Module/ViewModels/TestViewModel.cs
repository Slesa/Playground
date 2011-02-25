using Caliburn.Micro;
using NightOwl.Core;

namespace NightOwl.Test.Module.ViewModels
{
    public class TestViewModel : Screen, IModule
    {
        public TestViewModel()
        {
            DisplayName = "Test Module";
        }

        public string IconFileName
        {
            get { return "/NightOwl.Test.Module;component/Resources/TestView.png"; }
        }

        public string ToolTip
        {
            get { return "A test module to show a second view"; }
        }
    }
}