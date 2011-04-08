using Caliburn.Micro;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class UnitRowViewModel : PropertyChangedBase
    {
        readonly Unit _unit;

        public UnitRowViewModel(Unit unit)
        {
            _unit = unit;
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
                NotifyOfPropertyChange(()=>IsSelected);
            }
        }

        public int Id { get { return _unit.Id; } }
        public string Name { get { return _unit.Name; } }
        public string Contraction { get { return _unit.Contraction; } }
        public UnitType UnitType { get { return _unit.UnitType; } }
        public Unit Parent { get { return _unit.Parent; } }
        public decimal FactorToParent { get { return _unit.FactorToParent; } }
        public bool Purchasing { get { return _unit.Purchasing; } }
        public bool Reciping { get { return _unit.Reciping; } }
        
    }
}