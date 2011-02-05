using System.Linq;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class FindStockForRecipeItemQuery : IDomainQuery<Stock>
    {
        readonly RecipeItem _recipeItem;

        public FindStockForRecipeItemQuery(RecipeItem recipeItem)
        {
            _recipeItem = recipeItem;
        }

        public Stock Execute(ISession session)
        {
            var stockItems =
                session.Linq<StockItem>().Where(
                    x => x.RecipeableItem == _recipeItem.RecipeableItem && !x.Stock.IsMainStock);
            var stockItem = stockItems.FirstOrDefault();
            if (stockItem == null)
            {
                stockItems =
                    session.Linq<StockItem>().Where(
                        x => x.RecipeableItem == _recipeItem.RecipeableItem && x.Stock.IsMainStock);

                stockItem = stockItems.FirstOrDefault();
            }
            return stockItem == null ? null : stockItem.Stock;
        }
    }
}