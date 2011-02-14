using System.Collections.Generic;
using System.Linq;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsRunner.Core
{
    public class RecipeFinder : IRecipeFinder
    {
        readonly IDbConversation _dbConversation;

        public RecipeFinder(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
        }

        public IEnumerable<Recipe> FindRecipes(RecipeJob recipeJob)
        {
            List<Recipe> recipes = null;
            _dbConversation.UsingTransaction(() =>
                {
                    recipes = _dbConversation.Query(new FindRecipesForSalesItemQuery(recipeJob.SalesItem)).ToList();
                });
            return recipes;
        }
    }
}
