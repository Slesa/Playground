using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class PayformRowViewModel: SelectableRowViewModelBase<Payform>
    {
        public PayformRowViewModel(Payform payform)
        {
            ElementData = payform;
        }
        public void ExchangeData(Payform payform)
        {
            ElementData = payform;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        
    }
}