using System.ComponentModel;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleRecipeItemViewModel: ViewModelBase, IDataErrorInfo
    {
        private readonly EditRecipeItem _editRecipeItem;

        public SingleRecipeItemViewModel()
        {
            _editRecipeItem = EditRecipeItem.CreateRecipeItem();
        }

        public SingleRecipeItemViewModel(RecipeItem editRecipeItem)
        {
            _editRecipeItem = new EditRecipeItem(editRecipeItem);
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

        public decimal Quantity
        {
            get { return _editRecipeItem.Quantity; }
            set
            {
                if (value == _editRecipeItem.Quantity)
                    return;
                _editRecipeItem.Quantity = value;
                base.OnPropertyChanged("Quantity");
            }
        }

        public Unit Unit
        {
            get { return _editRecipeItem.Unit; }
            set
            {
                if (value == _editRecipeItem.Unit)
                    return;
                _editRecipeItem.Unit = value;
                base.OnPropertyChanged("Unit");
            }
        }

        public string RecipeableItemType
        {
            get { return _editRecipeItem.RecipeableItem.GetRecipeableItemType(); }
        }

        public RecipeableItem RecipeableItem
        {
            get { return _editRecipeItem.RecipeableItem; }
            set
            {
                if (value == _editRecipeItem.RecipeableItem)
                    return;
                _editRecipeItem.RecipeableItem = value;
                base.OnPropertyChanged("RecipeableItem");
                base.OnPropertyChanged("RecipeableItemType");
                if( _editRecipeItem.RecipeableItem!=null )
                    Unit = _editRecipeItem.RecipeableItem.RecipeUnit;
            }
        }

        public RecipeItem UnderlayingObject()
        {
            return _editRecipeItem.RecipeItem;
        }

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                // HACK: Validierung aus
                return null;
                var error = (_editRecipeItem as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            // HACK: Validierung aus
            get { return null;  return (_editRecipeItem as IDataErrorInfo).Error; }
        }

        #endregion

    }
        
}