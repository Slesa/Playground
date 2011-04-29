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
    public class ListSalesFamiliesViewModel : SelectionListViewModel<SalesFamilyRowViewModel>, IPmsModule
        , IHandle<SalesFamilyChangedEvent>
        , IHandle<SalesFamilyRemovedEvent>
    {
        public ListSalesFamiliesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(Strings.SalesFamiliesModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditSalesFamilyViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var salesFamily in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditSalesFamilyViewModel(salesFamily.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllSalesFamiliesView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, salesFamily) => current + string.Format("{0} {1}", salesFamily.Id, salesFamily.Name));

            if (MessageBox.Show(message, Strings.AllSalesFamiliesView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new SalesFamilyRemovedEvent {Id = t.Id});
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.SalesFamiliesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/SalesFamily.png"; }
        }

        public string ToolTip
        {
            get { return Strings.SalesFamiliesTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<SalesFamilyRowViewModel> CreateElementList()
        {
            return new BindableCollection<SalesFamilyRowViewModel>(DbConversation
                .Query(new AllSalesFamiliesQuery())
                .Select(x => new SalesFamilyRowViewModel(x)));
        }

        public void Handle(SalesFamilyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.SalesFamily.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SalesFamilyRowViewModel(message.SalesFamily);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.SalesFamily);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(SalesFamilyRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}