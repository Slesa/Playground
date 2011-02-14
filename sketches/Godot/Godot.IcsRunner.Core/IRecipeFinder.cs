using System.Collections.Generic;
using Godot.IcsModel.Entities;

namespace Godot.IcsRunner.Core
{
    public interface IRecipeFinder
    {
        IEnumerable<Recipe> FindRecipes(RecipeJob recipeJob);
    }
}