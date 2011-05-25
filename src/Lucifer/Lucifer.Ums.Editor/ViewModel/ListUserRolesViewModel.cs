using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Queries;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUserRolesViewModel : SelectionListViewModel<UserRoleRowViewModel>, IUmsModule
        , IHandle<UserRoleChangedEvent>
        , IHandle<UserRoleRemovedEvent>
    {
        public ListUserRolesViewModel()
            : base(Strings.UserRolesModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUserRoleViewModel());
        }

        public void Edit()
        {
            foreach (var userRole in ElementList.Where(row => row.IsSelected))
                ScreenManager.ActivateItem(new EditUserRoleViewModel(userRole.Id));
        }

        public void Open(UserRowViewModel viewModel)
        {
            ScreenManager.ActivateItem(new EditUserViewModel(viewModel.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllUserRolesView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, unitType) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", unitType.Id, unitType.Name));

                var question = new QuestionViewModel(Strings.AllUserRolesView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new UserRoleRemovedEvent(t.Id));
                }
            }
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