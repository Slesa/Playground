using Castle.MicroKernel.Facilities;

namespace Godot.Infrastructure.Container
{
	public class ArrayDependencyFacility : AbstractFacility
	{
		protected override void Init()
		{
			Kernel.Resolver.AddSubResolver(new ArraySubDependencyResolver(Kernel));
		}
	}
}