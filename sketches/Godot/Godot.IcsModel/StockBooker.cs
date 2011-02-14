using System;
using System.Linq;
using Godot.IcsModel.Entities;

namespace Godot.IcsModel
{
    public class StockBooker : IStockBooker
    {
        readonly IUnitConverter _unitConverter;

        public StockBooker(IUnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public StockItem BookItemIntoStock(Stock stock, decimal quantity, Unit unit, RecipeableItem recipeableItem)
        {
            if (stock == null)
                throw new ArgumentNullException("stock");
            
            var stockItem = FindStockItem(stock, unit, recipeableItem, true);
            var bookQuantity = _unitConverter.Convert(quantity, unit, stockItem.Unit);
            stockItem.Quantity += bookQuantity;
            return stockItem;
        }

        public StockItem BookItemOutOfStock(Stock stock, decimal quantity, Unit unit, RecipeableItem recipeableItem)
        {
            if (stock == null)
                throw new ArgumentNullException("stock");

            var stockItem = FindStockItem(stock, unit, recipeableItem, stock.IsMainStock);
            var bookQuantity = _unitConverter.Convert(quantity, unit, stockItem.Unit);
            stockItem.Quantity -= bookQuantity;
            return stockItem;
        }

        internal StockItem FindStockItem(Stock stock, Unit unit, RecipeableItem recipeableItem, bool createIfNotExists)
        {
            if (recipeableItem == null)
                throw new ArgumentNullException("recipeableItem");
            if (unit == null)
                throw new ArgumentNullException("unit");

            var stockItem = stock.StockItems.Where(x => x.RecipeableItem == recipeableItem).FirstOrDefault();
            if (stockItem == null)
            {
                if( !createIfNotExists )
                    throw new InvalidOperationException();
                stockItem = new StockItem { RecipeableItem = recipeableItem, Unit = unit };
                stock.AddStockItem(stockItem);
            }
            return stockItem;
        }
    }
}