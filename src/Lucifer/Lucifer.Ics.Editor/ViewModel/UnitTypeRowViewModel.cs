using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class UnitTypeRowViewModel : SelectableRowViewModelBase<UnitType>
    {
        public UnitTypeRowViewModel(UnitType unitType)
        {
            ElementData = unitType;
        }
        public void ExchangeData(UnitType unitType)
        {
            ElementData = unitType;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        
    }
}