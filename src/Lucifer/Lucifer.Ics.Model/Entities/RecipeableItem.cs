using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class RecipeableItem : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual Unit RecipeUnit { get; set; }
    }
}