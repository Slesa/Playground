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
    public class ListSalesItemsViewModel : SelectionListViewModel<SalesItemRowViewModel>, IPmsModule
        , IHandle<SalesItemChangedEvent>
        , IHandle<SalesItemRemovedEvent>
        , IHandle<SalesFamilyChangedEvent>
    {
        public ListSalesItemsViewModel() 
            : base(Strings.SalesItemsModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditSalesItemViewModel());
        }

        public void Edit()
        {
            foreach (var salesItem in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditSalesItemViewModel(salesItem.Id));
        }

        public void Open(SalesItemRowViewModel viewModel)
        {
            ScreenManager.ActivateItem(new EditSalesItemViewModel(viewModel.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllSalesItemsView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, salesItem) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", salesItem.Id, salesItem.Name));

                var question = new QuestionViewModel(Strings.AllSalesItemsView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new SalesItemRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.SalesItemsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/SalesItem.png"; }
        }

        public string ToolTip
        {
            get { return Strings.SalesItemsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<SalesItemRowViewModel> CreateElementList()
        {
            return new BindableCollection<SalesItemRowViewModel>(DbConversation
                .Query(new AllSalesItemsQuery())
                .Select(x => new SalesItemRowViewModel(x)));
        }

        public void Handle(SalesItemChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.SalesItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SalesItemRowViewModel(message.SalesItem);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.SalesItem);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(SalesItemRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
        public void Handle(SalesFamilyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.SalesFamily== message.SalesFamily select vm);
            viewmodel.Each(x =>
            {
                x.SalesFamily = message.SalesFamily;
                x.Refresh();
            });
        }
    }
}