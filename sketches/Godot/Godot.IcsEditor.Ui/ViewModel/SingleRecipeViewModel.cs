using System.Linq;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleRecipeViewModel : ViewModelBase
    {
        private Recipe _recipe;

        public SingleRecipeViewModel(Recipe recipe)
        {
            _recipe = recipe;
            base.DisplayName = _recipe.Plu.ToString();
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;
                _isSelected = value;
                base.OnPropertyChanged("IsSelected");
            }
        }

        public int Id
        {
            get { return _recipe.Id; }
        }

        public int Plu
        {
            get { return _recipe.Plu; }
        }

        public string ItemNames
        {
            get
            {
                var names = _recipe.RecipeItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(5);
                return string.Join(", ", names);
            }
        }

        public void ExchangeData(Recipe recipe)
        {
            _recipe = recipe;
        }

        public Recipe UnderlayingObject()
        {
            return _recipe;
        }
    }
}