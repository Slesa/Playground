using System.Collections.ObjectModel;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditStockMovementItem
    {
        public EditStockMovementItem(RecipeableItem itemToBook, Unit unitToBook)
        {
            ItemToBook = itemToBook;
            UnitToBook = unitToBook;
            QuantityToBook = 0.0m;
            QuantityLeft = 0.0m;
        }

        public decimal QuantityToBook { get; set; }
        public RecipeableItem ItemToBook { get; set; }
        public Unit UnitToBook { get; set; }
        public decimal QuantityLeft { get; set; }
    }

    public class EditStockMovements
    {
        readonly IDbConversation _dbConversation;

        protected EditStockMovements(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
        }

        public ObservableCollection<EditStockMovementItem> GetItemsToMove(Stock stock)
        {
            var items = new ObservableCollection<EditStockMovementItem>();
            if (stock != null)
            {
                if (!stock.IsMainStock)
                {
                    stock.StockItems.Each(
                        si =>
                        items.Add(new EditStockMovementItem(si.RecipeableItem, si.Unit) {QuantityLeft = si.Quantity}));
                }
                else
                {
                    var query = _dbConversation.Query(new AllRecipeableItemsQuery());
                    query.Each(
                        ri => 
                        items.Add(new EditStockMovementItem(ri, ri.RecipeUnit)));
                }
            }
            return items;
        }

    }
}