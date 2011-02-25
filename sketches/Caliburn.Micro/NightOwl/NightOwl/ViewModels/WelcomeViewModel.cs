using Caliburn.Micro;
using NightOwl.Core;
using NightOwl.Resources;

namespace NightOwl.ViewModels
{
    public class WelcomeViewModel : Screen, IModule
    {
        public WelcomeViewModel()
        {
            DisplayName = AppStrings.WelcomeTitle;
        }

        public string IconFileName
        {
            get { return "/NightOwl;component/Resources/WelcomeView.png"; }
        }

        public string ToolTip
        {
            get { return "Welcome view to have at least one view"; }
        }
    }
}