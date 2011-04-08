using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class ListUserRolesViewModel : Screen, IUmsModule
    {
        public ListUserRolesViewModel()
        {
            DisplayName = Strings.UserRolesModule;
            CreateAllUserRoles();
        }

        public ObservableCollection<User> AllUserRoles;

        void CreateAllUserRoles()
        {
            AllUserRoles = new ObservableCollection<User>
                {
                    new User {Name = "User Role 1"},
                };
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