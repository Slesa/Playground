using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SinglePurchaseItemViewModel : ViewModelBase
    {
        private PurchaseItem _purchaseItem;

        public SinglePurchaseItemViewModel(PurchaseItem purchaseItem)
        {
            _purchaseItem = purchaseItem;
            base.DisplayName = purchaseItem.Name;
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
            get { return _purchaseItem.Id; }
        }

        public string Name
        {
            get { return _purchaseItem.Name; }
        }

        public PurchaseFamily PurchaseFamily
        {
            get { return _purchaseItem.PurchaseFamily; }
        }

        public Unit PurchaseUnit
        {
            get { return _purchaseItem.PurchaseUnit; }
        }

        public Unit RecipeUnit
        {
            get { return _purchaseItem.RecipeUnit; }
        }

        public void ExchangeData(PurchaseItem purchaseItem)
        {
            _purchaseItem = purchaseItem;
        }

        public PurchaseItem UnderlayingObject()
        {
            return _purchaseItem;
        }
    }
}