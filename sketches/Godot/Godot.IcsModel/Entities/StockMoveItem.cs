using Godot.Model;

namespace Godot.IcsModel.Entities
{
    public class StockMoveItem : DomainEntity
    {
        public virtual RecipeableItem RecipeableItem { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual StockMovement StockMovement { get; set; }
    }
}