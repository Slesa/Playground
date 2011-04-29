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
    public class ListPurchaseItemsViewModel : SelectionListViewModel<PurchaseItemRowViewModel>, IIcsModule
        , IHandle<PurchaseItemChangedEvent>
        , IHandle<PurchaseItemRemovedEvent>
        , IHandle<PurchaseFamilyChangedEvent>
        , IHandle<UnitChangedEvent>
    {
        public ListPurchaseItemsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.PurchaseItemsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditPurchaseItemViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var purchaseItem in ElementList.Where(pi => pi.IsSelected))
                ScreenManager.ActivateItem(new EditPurchaseItemViewModel(purchaseItem.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllPurchaseItemsView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, pi) => current + string.Format("{0} {1}", pi.Id, pi.Name));

            if (MessageBox.Show(message, Strings.AllPurchaseItemsView_RemoveTitle, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new PurchaseItemRemovedEvent { Id = t.Id });
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.PurchaseItemsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/PurchaseItem.png"; }
        }

        public string ToolTip
        {
            get { return Strings.PurchaseItemsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<PurchaseItemRowViewModel> CreateElementList()
        {
            return new BindableCollection<PurchaseItemRowViewModel>(DbConversation
                .Query(new AllPurchaseItemsQuery())
                .Select(x => new PurchaseItemRowViewModel(x)));
        }

        public void Handle(PurchaseItemChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.PurchaseItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new PurchaseItemRowViewModel(message.PurchaseItem);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.PurchaseItem);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(PurchaseItemRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
        public void Handle(PurchaseFamilyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.PurchaseFamily == message.PurchaseFamily select vm);
            viewmodel.Each(x =>
            {
                x.PurchaseFamily = message.PurchaseFamily;
                x.Refresh();
            });
        }
        public void Handle(UnitChangedEvent message)
        {
            var purchaseViews = (from vm in ElementList where vm.PurchaseUnit == message.Unit select vm);
            purchaseViews.Each(x =>
            {
                x.PurchaseUnit = message.Unit;
                x.Refresh();
            });
            var recipeViews = (from vm in ElementList where vm.RecipeUnit == message.Unit select vm);
            purchaseViews.Each(x =>
            {
                x.RecipeUnit = message.Unit;
                x.Refresh();
            });
        }
       
    }
}