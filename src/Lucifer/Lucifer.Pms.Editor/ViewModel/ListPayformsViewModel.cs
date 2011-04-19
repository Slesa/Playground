using System.Collections.ObjectModel;
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
    public class ListPayformsViewModel : SelectionListViewModel<PayformRowViewModel>, IPmsModule
        , IHandle<PayformChangedEvent>
        , IHandle<PayformRemovedEvent>
    {
        public ListPayformsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.PayformsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditPayformViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var payform in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditPayformViewModel(payform.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllPayformsView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, unitType) => current + string.Format("{0} {1}", unitType.Id, unitType.Name));

            if (MessageBox.Show(message, Strings.AllPayformsView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new PayformRemovedEvent { Id = t.Id });
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.PayformsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Payform.png"; }
        }

        public string ToolTip
        {
            get { return Strings.PayformsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override ObservableCollection<PayformRowViewModel> CreateElementList()
        {
            return new ObservableCollection<PayformRowViewModel>(DbConversation
                .Query(new AllPayformsQuery())
                .Select(x => new PayformRowViewModel(x)));
        }

        public void Handle(PayformChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Payform.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new PayformRowViewModel(message.Payform);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Payform);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(PayformRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}