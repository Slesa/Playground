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
    public class ListUsersViewModel : SelectionListViewModel<UserRowViewModel>, IUmsModule
        , IHandle<UserChangedEvent>
        , IHandle<UserRemovedEvent>
        , IHandle<UserRoleChangedEvent>
    {
        public ListUsersViewModel()
            : base(Strings.UsersModule)
        {
            EventAggregator.Subscribe(this);
        }


        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUserViewModel());
        }

        public void Edit()
        {
            foreach (var user in ElementList.Where(row => row.IsSelected))
                ScreenManager.ActivateItem(new EditUserViewModel(user.Id));
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
                var message = Strings.AllUsersView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, unitType) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", unitType.Id, unitType.Name));

                var question = new QuestionViewModel(Strings.AllUsersView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new UserRemovedEvent(t.Id));
                }
            }
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