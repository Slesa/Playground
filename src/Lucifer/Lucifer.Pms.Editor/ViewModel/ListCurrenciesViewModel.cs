using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListCurrenciesViewModel : SelectionListViewModel<CurrencyRowViewModel>, IPmsModule
        , IHandle<CurrencyChangedEvent>
        , IHandle<CurrencyRemovedEvent>
    {
        public ListCurrenciesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(Strings.CurrenciesModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditCurrencyViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var currency in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditCurrencyViewModel(currency.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllCurrenciesView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, currency) => current + string.Format("{0} {1}", currency.Id, currency.Name));

            if (MessageBox.Show(message, Strings.AllCurrenciesView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new CurrencyRemovedEvent {Id = t.Id});
        }

        #region IIcsModule

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

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<CurrencyRowViewModel> CreateElementList()
        {
            return new BindableCollection<CurrencyRowViewModel>(DbConversation
                .Query(new AllCurrenciesQuery())
                .Select(x => new CurrencyRowViewModel(x)));
        }

        public void Handle(CurrencyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Currency.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new CurrencyRowViewModel(message.Currency);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Currency);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(CurrencyRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}