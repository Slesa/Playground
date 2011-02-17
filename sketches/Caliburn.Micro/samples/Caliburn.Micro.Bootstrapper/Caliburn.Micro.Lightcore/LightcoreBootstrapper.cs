using System;
using LightCore;

namespace Caliburn.Micro.Bootstrapper
{
    public class LightcoreBootstrapper : Bootstrapper<IShell>
    {
        IContainer _container;

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.Register<IWindowManager,WindowManager>();
            builder.Register<IEventAggregator, EventAggregator>();
            builder.Register(_container);
            builder.Register<IShell, ShellViewModel>();

            _container = builder.Build();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var export = string.IsNullOrEmpty(key) ? _container.Resolve(serviceType) : _container.Resolve(Type.GetType(key));
            if (export != null)
                return export;

            var contract = string.IsNullOrEmpty(key) ? serviceType.Name : key;
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override System.Collections.Generic.IEnumerable<object> GetAllInstances(Type service)
        {
            yield return _container.ResolveAll(service).GetEnumerator();
        }

        //protected override void BuildUp(object instance)
        //{
        //    _container.SatisfyImportsOnce(instance);
        //}
    }
}