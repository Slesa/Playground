using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Queries;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUserRolesViewModel : Screen, IUmsModule
    {
        readonly IDbConversation _dbConversation;

        public ListUserRolesViewModel(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = Strings.UserRolesModule;
            CreateAllUserRoles();
        }

        public ObservableCollection<UserRoleRowViewModel> AllUserRoles { get; private set; }

        void CreateAllUserRoles()
        {
            AllUserRoles = new ObservableCollection<UserRoleRowViewModel>(_dbConversation
                .Query(new AllUserRolesQuery())
                .Select(x=>new UserRoleRowViewModel(x)));
            /*AllUserRoles = new ObservableCollection<UserRoleRowViewModel>
                {
                    new UserRoleRowViewModel(new UserRole {Name = "User Role 1"}),
                };*/
        }

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
    }
}