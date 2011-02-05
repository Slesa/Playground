using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Godot.Infrastructure;

namespace Godot.IcsModel
{
    public class IcsModelRegistrationsContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IUnitConverter>()
                .ImplementedBy<UnitConverter>();

            yield return Component
                .For<IStockBooker>()
                .ImplementedBy<StockBooker>();

        }
    }
}