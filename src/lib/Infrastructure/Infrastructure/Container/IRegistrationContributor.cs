using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace Infrastructure.Container
{
    public interface IRegistrationContributor
    {
        IEnumerable<IRegistration> GetRegistrations();
    }
}