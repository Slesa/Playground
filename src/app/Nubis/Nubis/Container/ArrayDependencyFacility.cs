using Castle.MicroKernel.Facilities;

namespace Nubis.Container
{
	public class ArrayDependencyFacility : AbstractFacility
	{
		protected override void Init()
		{
			Kernel.Resolver.AddSubResolver(new ArraySubDependencyResolver(Kernel));
		}
	}
}
