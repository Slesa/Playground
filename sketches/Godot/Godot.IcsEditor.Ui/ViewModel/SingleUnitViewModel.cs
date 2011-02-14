using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class SingleUnitViewModel : ViewModelBase
    {
        private Unit _unit;

        public SingleUnitViewModel(Unit unit)
        {
            _unit = unit;
            base.DisplayName = unit.Name;
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
            get { return _unit.Id; }
        }

        public string Name
        {
            get { return _unit.Name; }
        }

        public string Contraction
        {
            get { return _unit.Contraction; }
        }

        public UnitType UnitType
        {
            get { return _unit.UnitType; }
        }

        public Unit Parent
        {
            get { return _unit.Parent; }
        }

        public decimal FactorToParent
        {
            get { return _unit.FactorToParent; }
        }

        public bool Purchasing
        {
            get { return _unit.Purchasing; }
        }

        public bool Reciping
        {
            get { return _unit.Reciping; }
        }

        public void ExchangeData(Unit unit)
        {
            _unit = unit;
        }

        public Unit UnderlayingObject()
        {
            return _unit;
        }
    }
}