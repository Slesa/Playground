using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Queries;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUsersViewModel : SelectionListViewModel<UserRowViewModel>, IUmsModule
        , IHandle<UserChangedEvent>
        , IHandle<UserRemovedEvent>
        , IHandle<UserRoleChangedEvent>
    {
        public ListUsersViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.UsersModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }


        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUserViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var payform in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditUserViewModel(payform.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllUsersView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, unitType) => current + string.Format("{0} {1}", unitType.Id, unitType.Name));

            if (MessageBox.Show(message, Strings.AllUsersView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new UserRemovedEvent(t.Id));
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.UsersModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/User.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UsersTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<UserRowViewModel> CreateElementList()
        {
            return new BindableCollection<UserRowViewModel>(DbConversation
                .Query(new AllUsersQuery())
                .Select(x => new UserRowViewModel(x)));
        }

        public void Handle(UserChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.User.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new UserRowViewModel(message.User);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.User);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(UserRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }

        public void Handle(UserRoleChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.UserRole == message.UserRole select vm);
            viewmodel.Each(x =>
            {
                x.UserRole = message.UserRole;
                x.Refresh();
            });
        }
    }
}