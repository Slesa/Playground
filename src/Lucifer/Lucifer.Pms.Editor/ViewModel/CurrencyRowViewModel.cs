using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class CurrencyRowViewModel : SelectableRowViewModelBase
    {
        readonly Currency _currency;

        public CurrencyRowViewModel(Currency currency)
        {
            _currency = currency;
        }

        public int Id { get { return _currency.Id; } }
        public string Name { get { return _currency.Name; } }
        
    }
}