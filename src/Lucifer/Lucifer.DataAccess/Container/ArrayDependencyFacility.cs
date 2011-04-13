using System;
using Castle.MicroKernel.Facilities;

namespace Lucifer.DataAccess.Container
{
    public class ArrayDependencyFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Resolver.AddSubResolver(new ArraySubDependencyResolver(Kernel));
        }
    }
}