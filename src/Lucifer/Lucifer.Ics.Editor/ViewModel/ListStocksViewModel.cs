using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListStocksViewModel : SelectionListViewModel<StockRowViewModel>, IIcsModule
        , IHandle<StockChangedEvent>
        , IHandle<StockRemovedEvent>
    {
        public ListStocksViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.StocksModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditStockViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var stock in ElementList.Where(pf => pf.IsSelected))
                ScreenManager.ActivateItem(new EditStockViewModel(stock.Id, DbConversation, EventAggregator));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllStocksView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, pf) => current
                                              + string.Format(CultureInfo.CurrentCulture, "{0} {1}", pf.Id, pf.Name));

                var question = new QuestionViewModel(Strings.AllStocksView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new StockRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.StocksModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Stock.png"; }
        }

        public string ToolTip
        {
            get { return Strings.StocksTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<StockRowViewModel> CreateElementList()
        {
            return new BindableCollection<StockRowViewModel>(DbConversation
                .Query(new AllStocksQuery())
                .Select(x => new StockRowViewModel(x)));
        }

        public void Handle(StockChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Stock.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new StockRowViewModel(message.Stock);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Stock);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(StockRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }

    }
}