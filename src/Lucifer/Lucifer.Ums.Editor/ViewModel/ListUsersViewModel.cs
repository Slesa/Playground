using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Queries;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUsersViewModel : Screen, IUmsModule
    {
        readonly IDbConversation _dbConversation;

        public ListUsersViewModel(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = Strings.UsersModule;
            CreateAllUsers();
        }

        public ObservableCollection<UserRowViewModel> AllUsers { get; private set; }

        void CreateAllUsers()
        {
            AllUsers = new ObservableCollection<UserRowViewModel>(_dbConversation
                .Query(new AllUsersQuery())
                .Select(x=>new UserRowViewModel(x)));
            /*AllUsers = new ObservableCollection<UserRowViewModel>
                {
                    new UserRowViewModel(new User {Name = "User 1"}),
                };*/
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