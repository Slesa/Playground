using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class PayformRowViewModel: SelectableRowViewModelBase
    {
        readonly Payform _payform;

        public PayformRowViewModel(Payform payform)
        {
            _payform = payform;
        }

        public int Id { get { return _payform.Id; } }
        public string Name { get { return _payform.Name; } }
        
    }
}