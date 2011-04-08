using System.Collections.ObjectModel;
using Caliburn.Micro;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUsersViewModel : Screen, IUmsModule
    {
        public ListUsersViewModel()
        {
            DisplayName = Strings.UsersModule;
            CreateAllUsers();
        }

        public ObservableCollection<UserRowViewModel> AllUsers { get; private set; }

        void CreateAllUsers()
        {
            AllUsers = new ObservableCollection<UserRowViewModel>
                {
                    new UserRowViewModel(new User {Name = "User 1"}),
                };
        }

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
    }
}