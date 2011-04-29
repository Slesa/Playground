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
    public class ListUserRolesViewModel : SelectionListViewModel<UserRoleRowViewModel>, IUmsModule
        , IHandle<UserRoleChangedEvent>
        , IHandle<UserRoleRemovedEvent>
    {
        public ListUserRolesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.UserRolesModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUserRoleViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var payform in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditUserRoleViewModel(payform.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllUserRolesView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, unitType) => current + string.Format("{0} {1}", unitType.Id, unitType.Name));

            if (MessageBox.Show(message, Strings.AllUserRolesView_RemoveTitle, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new UserRoleRemovedEvent { Id = t.Id });
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.UserRolesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/UserRole.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UserRolesTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<UserRoleRowViewModel> CreateElementList()
        {
            return new BindableCollection<UserRoleRowViewModel>(DbConversation
                .Query(new AllUserRolesQuery())
                .Select(x => new UserRoleRowViewModel(x)));
        }

        public void Handle(UserRoleChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.UserRole.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new UserRoleRowViewModel(message.UserRole);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.UserRole);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(UserRoleRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}