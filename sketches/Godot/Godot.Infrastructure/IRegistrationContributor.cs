using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace Godot.Infrastructure
{
    public interface IRegistrationContributor
    {
        IEnumerable<IRegistration> GetRegistrations();
    }
}