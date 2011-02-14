using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Caliburn.Micro.HelloPhone7
{
    public class Wp7Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer(this);
            _container.RegisterPerRequest(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            _container.RegisterPerRequest(typeof(PageTwoViewModel), "PageTwoViewModel", typeof(PageTwoViewModel));
            _container.RegisterSingleton(typeof(TabViewModel), null, typeof(TabViewModel));

            _container.RegisterInstance(typeof(INavigationService), null, new FrameAdapter(RootFrame));
            _container.RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(PhoneService));

            _container.Activator.InstallChooser<PhoneNumberChooserTask, PhoneNumberResult>();
            _container.Activator.InstallLauncher<EmailComposeTask>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                    {
                        ConventionManager
                            .GetElementConvention(typeof (ItemsControl))
                            .ApplyBinding(viewModelType, path, property, element, convention);
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, viewModelType);
                    };
            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                    {
                        ConventionManager
                            .GetElementConvention(typeof (ItemsControl))
                            .ApplyBinding(viewModelType, path, property, element, convention);
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, viewModelType);
                    };
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}