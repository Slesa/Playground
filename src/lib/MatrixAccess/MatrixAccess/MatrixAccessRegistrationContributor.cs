using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Infrastructure.Container;

namespace MatrixAccess
{
    public class MatrixAccessRegistrationContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IDatFileNameMapper>()
                .ImplementedBy<DatFileNameMapper>();

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IMatrixFileLoader<>))
                .WithService.Base();

        }
    }
}