using System;
using System.Collections.Generic;
using Microsoft.Phone.Tasks;

namespace Caliburn.Micro.HelloWp7
{
    public class HelloWp7Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer();

            _container.RegisterSingleton(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            _container.RegisterSingleton(typeof(PageTwoViewModel), "PageTwoViewModel", typeof (PageTwoViewModel));
            _container.RegisterPerRequest(typeof(TabViewModel), null, typeof(TabViewModel));

            _container.RegisterInstance(typeof (INavigationService), null, new FrameAdapter(RootFrame));
            _container.RegisterInstance(typeof (IPhoneService), null, new PhoneApplicationServiceAdapter(PhoneService));

            _container.Activator.InstallChooser<PhoneNumberChooserTask, PhoneNumberResult>();
            _container.Activator.InstallLauncher<EmailComposeTask>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}