using System.ComponentModel.Composition;
using Caliburn.Micro;
using NightHawkSL.Resources;

namespace NightHawkSL.ViewModels
{
    [Export(typeof(WelcomeViewModel))]
    public class WelcomeViewModel : Screen
    {
        public WelcomeViewModel()
        {
            DisplayName = AppStrings.WelcomeTitle;
        }
    }
}