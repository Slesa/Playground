using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListCurrenciesViewModel : Screen, IPmsModule
    {
        readonly IDbConversation _dbConversation;

        public ListCurrenciesViewModel(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = Strings.CurrenciesModule;
            CreateAllCurrencies();
        }

        public ObservableCollection<CurrencyRowViewModel> AllCurrencies { get; private set; }

        void CreateAllCurrencies()
        {
            AllCurrencies = new ObservableCollection<CurrencyRowViewModel>(_dbConversation
                .Query(new AllCurrenciesQuery())
                .Select(x=>new CurrencyRowViewModel(x)));
            /*AllCurrencies = new ObservableCollection<CurrencyRowViewModel>
                {
                    new CurrencyRowViewModel(new Currency {Name = "Currency 1"}),
                };*/
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