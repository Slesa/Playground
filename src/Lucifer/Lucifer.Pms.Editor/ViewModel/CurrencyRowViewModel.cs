using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class CurrencyRowViewModel : SelectableRowViewModelBase<Currency>
    {
        public CurrencyRowViewModel(Currency currency)
        {
            ElementData = currency;
        }
        public void ExchangeData(Currency currency)
        {
            ElementData = currency;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public string Contraction { get { return ElementData.Contraction; } }
        public string Symbol { get { return ElementData.Symbol; } }
        
    }
}