using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListPurchaseFamiliesViewModel : SelectionListViewModel<PurchaseFamilyRowViewModel>, IIcsModule
        , IHandle<PurchaseFamilyChangedEvent>
        , IHandle<PurchaseFamilyRemovedEvent>
    {
        public ListPurchaseFamiliesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.PurchaseFamiliesModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditPurchaseFamilyViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var purchaseFamily in ElementList.Where(pf => pf.IsSelected))
                ScreenManager.ActivateItem(new EditPurchaseFamilyViewModel(purchaseFamily.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllPurchaseFamiliesView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, pf) => current + string.Format("{0} {1}", pf.Id, pf.Name));

            if (MessageBox.Show(message, Strings.AllPurchaseFamiliesView_RemoveTitle, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new PurchaseFamilyRemovedEvent { Id = t.Id });
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.PurchaseFamiliesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/PurchaseFamily.png"; }
        }

        public string ToolTip
        {
            get { return Strings.PurchaseFamiliesTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<PurchaseFamilyRowViewModel> CreateElementList()
        {
            return new BindableCollection<PurchaseFamilyRowViewModel>(DbConversation
                .Query(new AllPurchaseFamiliesQuery())
                .Select(x => new PurchaseFamilyRowViewModel(x)));
        }

        public void Handle(PurchaseFamilyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.PurchaseFamily.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new PurchaseFamilyRowViewModel(message.PurchaseFamily);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.PurchaseFamily);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(PurchaseFamilyRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}