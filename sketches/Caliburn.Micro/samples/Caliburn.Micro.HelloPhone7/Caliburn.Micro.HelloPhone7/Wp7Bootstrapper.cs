namespace Caliburn.Micro.HelloPhone7
{
    public class Wp7Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer();
            _container.RegisterPerRequest(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            _container.RegisterPerRequest(typeof(PageTwoViewModel), "PageTwoViewModel", typeof(PageTwoViewModel));
            _container.RegisterSingleton()
        }
    }
}