using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsRunner.Core
{
    public class StockFinder : IStockFinder
    {
        readonly IDbConversation _dbConversation;

        public StockFinder(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
        }

        public Stock FindStockFor(RecipeJob recipeJob, RecipeItem recipeItem)
        {
            Stock stock = null;
            _dbConversation.UsingTransaction(()=>
                {
                    stock = _dbConversation.Query(new FindStockForRecipeItemQuery(recipeItem));
                });
            return stock;
        }
    }
}
