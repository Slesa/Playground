using System;
using System.Collections.Generic;
using Caliburn.Micro;
using LightCore;

namespace Caliburn.LightCore
{
    public class LightCoreBootstrapper : Bootstrapper<IShell>
    {
        IContainer _lightCoreContainer;

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register<IWindowManager, WindowManager>();
            builder.Register<IEventAggregator, EventAggregator>();

            builder.Register<IShell, ShellViewModel>();

            _lightCoreContainer = builder.Build();

        }

        protected override object GetInstance(Type service, string key)
        {

            return string.IsNullOrWhiteSpace(key) ? _lightCoreContainer.Resolve(service) : null; // : _lightCoreContainer.Resolve<service.GetInterface(key)>();
        }


        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _lightCoreContainer.ResolveAll(service);
        }
    }
}