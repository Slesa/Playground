using System.Collections.ObjectModel;
using Caliburn.Micro;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListCurrenciesViewModel : Screen, IPmsModule
    {
        public ListCurrenciesViewModel()
        {
            DisplayName = Strings.CurrenciesModule;
            CreateAllCurrencies();
        }

        public ObservableCollection<CurrencyRowViewModel> AllCurrencies { get; private set; }

        void CreateAllCurrencies()
        {
            AllCurrencies = new ObservableCollection<CurrencyRowViewModel>
                {
                    new CurrencyRowViewModel(new Currency {Name = "Currency 1"}),
                };
        }

        public string ModuleName
        {
            get { return Strings.CurrenciesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Currency.png"; }
        }

        public string ToolTip
        {
            get { return Strings.CurrenciesTooltip; }
        }
    }
}