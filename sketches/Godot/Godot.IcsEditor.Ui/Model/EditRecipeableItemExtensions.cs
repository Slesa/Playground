using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public static class EditRecipeableItemExtensions
    {
        public static string GetRecipeableItemType(this RecipeableItem item)
        {
            if (item != null)
            {
                if (item is PurchaseItem)
                    return "I";
                if (item is ProductionItem )
                    return "P";
            }
            return null;
        }
    }
}