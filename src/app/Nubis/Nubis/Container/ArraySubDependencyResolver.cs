using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace Nubis.Container
{
    public class ArraySubDependencyResolver : ISubDependencyResolver
    {
        readonly IKernel _kernel;

        public ArraySubDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return _kernel.ResolveAll(dependency.TargetType.GetElementType(), null);
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType != null &&
                   dependency.TargetType.IsArray &&
                   dependency.TargetType.GetElementType().IsInterface &&
                   !model.Parameters.Contains(dependency.DependencyKey);
        }
    }
}