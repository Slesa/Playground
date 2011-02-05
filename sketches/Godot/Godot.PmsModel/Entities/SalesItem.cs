using Godot.Model;

namespace Godot.PmsModel.Entities
{
    public class SalesItem : DomainEntity
    {
        // Wird gebraucht zum Remappen beim Laden, da Id sonst protected wäre
        public virtual SalesItem SetInternalId(int value) 
        {
            base.Id = value;
            return this;
        }
        //public virtual int Plu { get; set; }
        public virtual string Name { get; set; }
    }
}