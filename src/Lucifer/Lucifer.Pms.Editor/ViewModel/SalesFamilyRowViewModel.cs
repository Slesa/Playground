using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class SalesFamilyRowViewModel : SelectableRowViewModelBase<SalesFamily>
    {
        public SalesFamilyRowViewModel(SalesFamily salesFamily)
        {
            ElementData = salesFamily;
        }
        public void ExchangeData(SalesFamily salesFamily)
        {
            ElementData = salesFamily;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
    }
}