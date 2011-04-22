using System.Collections.Generic;
using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class Recipe : DomainEntity
    {
        readonly IList<RecipeItem> _recipeItems = new List<RecipeItem>();

        public virtual int Plu { get; set; }

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