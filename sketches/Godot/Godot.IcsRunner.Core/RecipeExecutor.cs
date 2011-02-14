using System;
using Godot.IcsModel;
using Godot.IcsModel.Entities;
using Godot.Model;

namespace Godot.IcsRunner.Core
{
    public class RecipeExecutor : IRecipeExecutor
    {
        readonly IDbConversation _dbConversation;
        readonly IRecipeFinder _recipeFinder;
        readonly IStockFinder _stockFinder;
        readonly IStockBooker _stockBooker;

        public RecipeExecutor(IDbConversation dbConversation, IRecipeFinder recipeFinder, IStockFinder stockFinder, IStockBooker stockBooker)
        {
            _dbConversation = dbConversation;
            _recipeFinder = recipeFinder;
            _stockFinder = stockFinder;
            _stockBooker = stockBooker;
        }

        public void Execute(RecipeJob recipeJob)
        {
            var recipes = _recipeFinder.FindRecipes(recipeJob);
            if (recipes == null)
            {
                Console.WriteLine("Could not find recipe for {0}", recipeJob.SalesItem);
                return;
            }
            Console.WriteLine("Got recipe(s) for {0}", recipeJob.SalesItem);
            foreach(var recipe in recipes)
            {
                foreach (var recipeItem in recipe.RecipeItems)
                {
                    var stock = _stockFinder.FindStockFor(recipeJob, recipeItem);
                    if (stock == null)
                    {
                        Console.WriteLine("Could not find stock for {0}", recipeItem.RecipeableItem.Name);
                        continue;
                    }
                    Console.WriteLine("Got stock {0} for {1}", stock.Name, recipeItem.RecipeableItem.Name);
                    _dbConversation.UsingTransaction(()=>
                        {
                            var moveRecipe = new StockMoveRecipe {OfStock = stock, ExecutedAt = DateTime.Now, Recipe = recipe};

                            var quantity = recipeJob.Quantity * recipeItem.Quantity;
                            var fromStockItem = _stockBooker.BookItemOutOfStock(stock, quantity, recipeItem.Unit, recipeItem.RecipeableItem);
                            if (fromStockItem != null)
                                _dbConversation.InsertObjectOnCommit(fromStockItem);
                            var moveItem = new StockMoveItem
                            {
                                Unit = recipeItem.Unit,
                                Quantity = quantity,
                                RecipeableItem = recipeItem.RecipeableItem,
                            };
                            moveRecipe.AddMoveItem(moveItem);

                            _dbConversation.InsertObjectOnCommit(moveRecipe);
                        });
                }
            }
        }
    }

}
