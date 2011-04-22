using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;
using System.Linq;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class RecipeRowViewModel : SelectableRowViewModelBase<Recipe>
    {
        public RecipeRowViewModel(Recipe recipe)
        {
            ElementData = recipe;
        }
        public void ExchangeData(Recipe recipe)
        {
            ElementData = recipe;
        }

        public int Id { get { return ElementData.Id; } }
        public int Plu { get { return ElementData.Plu; } }
        public string ItemNames
        {
            get
            {
                var names = ElementData.RecipeItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(10);
                return string.Join(", ", names);
            }
        }
    }
}