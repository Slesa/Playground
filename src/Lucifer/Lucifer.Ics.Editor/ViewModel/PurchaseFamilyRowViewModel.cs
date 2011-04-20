using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class PurchaseFamilyRowViewModel : SelectableRowViewModelBase<PurchaseFamily>
    {
        public PurchaseFamilyRowViewModel(PurchaseFamily purchaseFamily)
        {
            ElementData = purchaseFamily;
        }
        public void ExchangeData(PurchaseFamily purchaseFamily)
        {
            ElementData = purchaseFamily;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }

    }
}