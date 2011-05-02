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
    public class ListDiscountsViewModel : SelectionListViewModel<DiscountRowViewModel>, IPmsModule
        , IHandle<DiscountChangedEvent>
        , IHandle<DiscountRemovedEvent>
    {
        public ListDiscountsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(Strings.DiscountsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditDiscountViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var discount in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditDiscountViewModel(discount.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllDiscountsView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, discount) => current + string.Format("{0} {1}", discount.Id, discount.Name));

            if (MessageBox.Show(message, Strings.AllDiscountsView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new DiscountRemovedEvent(t.Id));
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.DiscountsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Discount.png"; }
        }

        public string ToolTip
        {
            get { return Strings.DiscountsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<DiscountRowViewModel> CreateElementList()
        {
            return new BindableCollection<DiscountRowViewModel>(DbConversation
                .Query(new AllDiscountsQuery())
                .Select(x => new DiscountRowViewModel(x)));
        }

        public void Handle(DiscountChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Discount.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new DiscountRowViewModel(message.Discount);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Discount);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(DiscountRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}