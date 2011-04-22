using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class DiscountRowViewModel : SelectableRowViewModelBase<Discount>
    {
        public DiscountRowViewModel(Discount discount)
        {
            ElementData = discount;
        }
        public void ExchangeData(Discount discount)
        {
            ElementData = discount;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public decimal Rate { get { return ElementData.Rate; } }
    }
}