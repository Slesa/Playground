using Godot.Model;

namespace Godot.IcsModel.Entities
{
    public class RecipeableItem : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual Unit RecipeUnit { get; set; }
        
    }
}