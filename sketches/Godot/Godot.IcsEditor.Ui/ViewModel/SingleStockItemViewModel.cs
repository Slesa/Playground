using System.ComponentModel;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleStockItemViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly EditStockItem _editStockItem;

        public SingleStockItemViewModel()
        {
            _editStockItem = EditStockItem.CreateStockItem();
        }

        public SingleStockItemViewModel(StockItem editStockItem)
        {
            _editStockItem = new EditStockItem(editStockItem);
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
            get { return _editStockItem.Quantity; }
            set
            {
                if (value == _editStockItem.Quantity)
                    return;
                _editStockItem.Quantity = value;
                base.OnPropertyChanged("Quantity");
            }
        }

        public Unit Unit
        {
            get { return _editStockItem.Unit; }
            set
            {
                if (value == _editStockItem.Unit)
                    return;
                _editStockItem.Unit = value;
                base.OnPropertyChanged("Unit");
            }
        }

        public string RecipeableItemType
        {
            get { return _editStockItem.RecipeableItem.GetRecipeableItemType(); }
        }

        public RecipeableItem RecipeableItem
        {
            get { return _editStockItem.RecipeableItem; }
            set
            {
                if (value == _editStockItem.RecipeableItem)
                    return;
                _editStockItem.RecipeableItem = value;
                base.OnPropertyChanged("RecipeableItem");
                base.OnPropertyChanged("RecipeableItemType");
                if (_editStockItem.RecipeableItem != null)
                    Unit = _editStockItem.RecipeableItem.RecipeUnit;
            }
        }

        public StockItem UnderlayingObject()
        {
            return _editStockItem.StockItem;
        }

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                // HACK: Validierung aus
                var error = (_editStockItem as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get
            {
                // HACK: Validierung aus
                return (_editStockItem as IDataErrorInfo).Error;
            }
        }

        #endregion
    }
}