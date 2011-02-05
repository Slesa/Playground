using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SinglePurchaseFamilyViewModel : ViewModelBase
    {
        private PurchaseFamily _purchaseFamily;

        public SinglePurchaseFamilyViewModel(PurchaseFamily purchaseFamily)
        {
            _purchaseFamily = purchaseFamily;
            base.DisplayName = purchaseFamily.Name;
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
            get { return _purchaseFamily.Id; }
        }

        public string Name
        {
            get { return _purchaseFamily.Name; }
        }

        public void ExchangeData(PurchaseFamily purchaseFamily)
        {
            _purchaseFamily = purchaseFamily;
        }

        public PurchaseFamily UnderlayingObject()
        {
            return _purchaseFamily;
        }
    }
}