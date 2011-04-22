using System.Collections.ObjectModel;
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
    public class ListProductionItemsViewModel : SelectionListViewModel<ProductionItemRowViewModel>, IIcsModule
        , IHandle<ProductionItemChangedEvent>
        , IHandle<ProductionItemRemovedEvent>
    {
        public ListProductionItemsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.ProductionItemsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditProductionItemViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var productionItem in ElementList.Where(pf => pf.IsSelected))
                ScreenManager.ActivateItem(new EditProductionItemViewModel(productionItem.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllProductionItemsView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, pf) => current + string.Format("{0} {1}", pf.Id, pf.Name));

            if (MessageBox.Show(message, Strings.AllProductionItemsView_RemoveTitle, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new ProductionItemRemovedEvent { Id = t.Id });
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.ProductionItemsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/ProductionItem.png"; }
        }

        public string ToolTip
        {
            get { return Strings.ProductionItemsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override ObservableCollection<ProductionItemRowViewModel> CreateElementList()
        {
            return new ObservableCollection<ProductionItemRowViewModel>(DbConversation
                .Query(new AllProductionItemsQuery())
                .Select(x => new ProductionItemRowViewModel(x)));
        }

        public void Handle(ProductionItemChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.ProductionItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new ProductionItemRowViewModel(message.ProductionItem);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.ProductionItem);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(ProductionItemRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
        
    }
}