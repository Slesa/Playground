using Godot.IcsModel.Entities;

namespace Godot.IcsRunner.Core
{
    public interface IStockFinder
    {
        Stock FindStockFor(RecipeJob recipeJob, RecipeItem recipeItem);
    }
}
