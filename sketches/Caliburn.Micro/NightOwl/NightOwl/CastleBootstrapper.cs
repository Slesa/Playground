using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NightOwl.Core;
using NightOwl.ViewModels;

namespace NightOwl
{
    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        IWindsorContainer _container;

        protected override void Configure()
        {
            _container = new WindsorContainer();

            var registrations = GetRegistrations();
            foreach (var registration in registrations)
                _container.Register(registration);

        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var export = string.IsNullOrEmpty(key) ? _container.Resolve(serviceType) : _container.Resolve(key);
            if (export != null)
                return export;

            var contract = string.IsNullOrEmpty(key) ? serviceType.Name : key;
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            yield return _container.ResolveAll(service).GetEnumerator();
        }

        //protected override void BuildUp(object instance)
        //{
        //    //_container.Resolve(typeof(instance));
        //    //_container.SatisfyImportsOnce(instance);
        //}

        IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IWindowManager>()
                .ImplementedBy<WindowManager>();
            yield return Component
                .For<IEventAggregator>()
                .ImplementedBy<EventAggregator>();
            yield return Component
                .For<IWindsorContainer>()
                .Instance(_container);
            yield return AllTypes
                .FromAssemblyContaining<IModule>()
                .BasedOn<IModule>()
                .WithService.FromInterface(typeof(IModule));
            yield return Component
                .For<IShell>()
                .ImplementedBy<ShellViewModel>();
        }
    }
}