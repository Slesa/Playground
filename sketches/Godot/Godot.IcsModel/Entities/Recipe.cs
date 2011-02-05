using System.Collections.Generic;
using Godot.Model;

namespace Godot.IcsModel.Entities
{
    /// <summary>
    /// Ein Recipe ordnet einem SalesItem eine Rezeptur zu. Eine Rezeptur besteht hierbei aus einer Liste von RecipeItems.
    /// </summary>
    public class Recipe : DomainEntity
    {
        public virtual int Plu { get; set; }

        readonly IList<RecipeItem> _recipeItems = new List<RecipeItem>();

        public virtual IEnumerable<RecipeItem> RecipeItems
        {
            get { return _recipeItems; }
        }

        public virtual void AddRecipeItem(RecipeItem recipeItem)
        {
            recipeItem.Recipe = this;
            _recipeItems.Add(recipeItem);
        }

        public virtual void RemoveRecipeItem(RecipeItem recipeItem)
        {
            _recipeItems.Remove(recipeItem);
        }
    }
}