using Lucifer.Editor;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class UnitRowViewModel : SelectableRowViewModelBase<Unit>
    {
        public UnitRowViewModel(Unit unit)
        {
            ElementData = unit;
        }
        public void ExchangeData(Unit unit)
        {
            ElementData = unit;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public string Contraction { get { return ElementData.Contraction; } }
        public UnitType UnitType
        {
            get { return ElementData.UnitType; }
            set { ElementData.UnitType = value; }
        }

        public Unit Parent { get { return ElementData.Parent; } }
        public decimal FactorToParent { get { return ElementData.FactorToParent; } }
        public bool Purchasing { get { return ElementData.Purchasing; } }
        public bool Reciping { get { return ElementData.Reciping; } }

        public override string ToString()
        {
            var text = Name+", "+Contraction;
            if (Purchasing) text += ", " + Strings.UnitRowModel_Purchasing;
            if (Reciping) text += ", " + Strings.UnitRowModel_Reciping;
            return text;
        }
    }
}