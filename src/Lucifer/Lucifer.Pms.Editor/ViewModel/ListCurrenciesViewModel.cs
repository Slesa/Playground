using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListCurrenciesViewModel : SelectionListViewModel<CurrencyRowViewModel>, IPmsModule
        , IHandle<CurrencyChangedEvent>
        , IHandle<CurrencyRemovedEvent>
    {
        public ListCurrenciesViewModel() 
            : base(Strings.CurrenciesModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditCurrencyViewModel());
        }

        public void Edit()
        {
            foreach (var currency in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditCurrencyViewModel(currency.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllCurrenciesView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, currency) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", currency.Id, currency.Name));

                var question = new QuestionViewModel(Strings.AllCurrenciesView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new CurrencyRemovedEvent(t.Id));
                }
            }
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