using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Godot.Infrastructure;

namespace Godot.IcsRunner.Core
{
    public class IcsRunnerRegistrationsContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IStockFinder>()
                .ImplementedBy<StockFinder>();

            yield return Component
                .For<IRecipeFinder>()
                .ImplementedBy<RecipeFinder>();

            yield return Component
                .For<IRecipeExecutor>()
                .ImplementedBy<RecipeExecutor>();

        }
    }
}