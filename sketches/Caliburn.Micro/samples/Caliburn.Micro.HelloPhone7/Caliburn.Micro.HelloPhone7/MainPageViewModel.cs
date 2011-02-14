using System;

namespace Caliburn.Micro.HelloPhone7
{
    public class MainPageViewModel
    {
        readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void GotoPageTwo()
        {
            _navigationService.Navigate(new Uri("/PageTwo.xaml?NumberOfTabs=5", UriKind.RelativeOrAbsolute));
        }
    }
}