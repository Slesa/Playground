using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.Container
{
    public class WindsorServiceLocator : ServiceLocatorImplBase
    {
        readonly IWindsorContainer _container;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().AsEnumerable();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key != null && key.Trim().Length > 0)
            {
                return _container.Resolve(key, serviceType);
            }
            return _container.Resolve(serviceType);
        }
    }
}