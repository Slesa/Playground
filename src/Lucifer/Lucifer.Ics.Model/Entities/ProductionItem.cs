using System.Collections.Generic;

namespace Lucifer.Ics.Model.Entities
{
    public class ProductionItem : RecipeableItem
    {
        readonly IList<RecipeItem> _recipeItems = new List<RecipeItem>();

        public virtual IEnumerable<RecipeItem> RecipeItems
        {
            get { return _recipeItems; }
        }

        public virtual void AddRecipeItem(RecipeItem recipeItem)
        {
            recipeItem.ProductionItem = this;
            _recipeItems.Add(recipeItem);
        }

        public virtual void RemoveRecipeItem(RecipeItem recipeItem)
        {
            _recipeItems.Remove(recipeItem);
        }
    }
}