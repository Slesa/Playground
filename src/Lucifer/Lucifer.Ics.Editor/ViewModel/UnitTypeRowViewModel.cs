using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class UnitTypeRowViewModel : SelectableRowViewModelBase
    {
        readonly UnitType _unitType;

        public UnitTypeRowViewModel(UnitType unitType)
        {
            _unitType = unitType;
        }

        public int Id { get { return _unitType.Id; } }
        public string Name { get { return _unitType.Name; } }
        
    }
}