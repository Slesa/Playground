using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleUnitTypeViewModel : ViewModelBase
    {
        private UnitType _unitType;

        public SingleUnitTypeViewModel(UnitType unitType)
        {
            _unitType = unitType;
            base.DisplayName = unitType.Name;
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
            get { return _unitType.Id; }
        }

        public string Name
        {
            get { return _unitType.Name; }
        }

        public UnitType UnderlayingObject()
        {
            return _unitType;
        }

        public void ExchangeData(UnitType unitType)
        {
            _unitType = unitType;
        }
    }
}