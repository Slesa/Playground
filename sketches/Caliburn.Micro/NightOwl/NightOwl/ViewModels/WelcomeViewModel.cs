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
    }
}