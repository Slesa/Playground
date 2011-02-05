using System.Linq;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleProductionItemViewModel : ViewModelBase
    {
        private ProductionItem _productionItem;

        public SingleProductionItemViewModel(ProductionItem productionItem)
        {
            _productionItem = productionItem;
            base.DisplayName = productionItem.Name;
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
            get { return _productionItem.Id; }
        }

        public string Name
        {
            get { return _productionItem.Name; }
        }

        public Unit RecipeUnit
        {
            get { return _productionItem.RecipeUnit; }
        }

        public string ItemNames
        {
            get
            {
                var names = _productionItem.RecipeItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(5);
                return string.Join(", ", names);
            }
        }

        public void ExchangeData(ProductionItem productionItem)
        {
            _productionItem = productionItem;
        }

        public ProductionItem UnderlayingObject()
        {
            return _productionItem;
        }
    }
}