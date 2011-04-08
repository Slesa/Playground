using Caliburn.Micro;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class UnitTypeRowViewModel : PropertyChangedBase
    {
        readonly UnitType _unitType;

        public UnitTypeRowViewModel(UnitType unitType)
        {
            _unitType = unitType;
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

        public int Id { get { return _unitType.Id; } }
        public string Name { get { return _unitType.Name; } }
        
    }
}