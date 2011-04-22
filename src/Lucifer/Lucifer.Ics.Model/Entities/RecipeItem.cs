using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class RecipeItem : DomainEntity
    {
        public virtual Recipe Recipe { get; set; }
        public virtual ProductionItem ProductionItem { get; set; }
        public virtual RecipeableItem RecipeableItem { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual Unit Unit { get; set; }


        public RecipeItem(decimal quantity, RecipeableItem recipeableItem)
        {
            Quantity = quantity;
            RecipeableItem = recipeableItem;
        }

        // Um neue anzulegen, braucht man schon den Default-ctor
        public RecipeItem()
        {
        }
    }
}