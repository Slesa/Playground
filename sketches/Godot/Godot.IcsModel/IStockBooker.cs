using Godot.IcsModel.Entities;

namespace Godot.IcsModel
{
    public interface IStockBooker
    {
        StockItem BookItemIntoStock(Stock stock, decimal quantity, Unit unit, RecipeableItem recipeableItem);
        StockItem BookItemOutOfStock(Stock stock, decimal quantity, Unit unit, RecipeableItem recipeableItem);
    }
}