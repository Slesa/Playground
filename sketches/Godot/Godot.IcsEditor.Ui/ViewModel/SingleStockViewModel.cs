using System.Linq;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleStockViewModel : ViewModelBase
    {
        private Stock _stock;

        public SingleStockViewModel(Stock stock)
        {
            _stock = stock;
            base.DisplayName = stock.Name;
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
            get { return _stock.Id; }
        }

        public string Name
        {
            get { return _stock.Name; }
        }

        public string ItemNames
        {
            get
            {
                var names = _stock.StockItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(5);
                return string.Join(", ", names);
            }
        }

        public void ExchangeData(Stock stock)
        {
            _stock = stock;
        }

        public Stock UnderlayingObject()
        {
            return _stock;
        }
    }
}