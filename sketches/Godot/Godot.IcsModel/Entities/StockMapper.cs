using Godot.Model;

namespace Godot.IcsModel.Entities
{
    public class StockMapper : DomainEntity
    {
        public virtual int Costcenter { get; set; }
        public virtual RecipeableItem RecipeableItem { get; set; }
        public virtual Stock Stock { get; set; }
    }
}