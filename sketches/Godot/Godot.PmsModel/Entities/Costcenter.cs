using Godot.Model;

namespace Godot.PmsModel.Entities
{
    public class Costcenter : DomainEntity
    {
        // Wird gebraucht zum Remappen beim Laden, da Id sonst protected wäre
        public virtual Costcenter SetInternalId(int value)
        {
            base.Id = value;
            return this;
        }
        public virtual string Name { get; set; }
    }
}