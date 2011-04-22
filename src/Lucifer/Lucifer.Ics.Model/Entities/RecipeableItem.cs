using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class RecipeableItem : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual Unit RecipeUnit { get; set; }
    }

    public static class EditRecipeableItemExtensions
    {
        public static string GetRecipeableItemType(this RecipeableItem item)
        {
            if (item != null)
            {
                if (item is PurchaseItem)
                    return "I";
                if (item is ProductionItem)
                    return "P";
            }
            return null;
        }
    }
}