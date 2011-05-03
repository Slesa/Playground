using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;
using Lucifer.Ums.Model.Queries;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class EditUserViewModel : EditItemViewModel<UserModel>, IDataErrorInfo
        , IHandle<UserRoleChangedEvent>
        , IHandle<UserRoleRemovedEvent>
    {
        public EditUserViewModel() 
        {
            DisplayName = Strings.EditUserView_NewUser;
            Title = Strings.EditUserView_TitleNew;
            ToolTip = Strings.AllUsersView_New_ToolTip;
        }

        public EditUserViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditUserView_UserIs, Element.Name);
            ToolTip = Strings.AllUsersView_Edit_ToolTip;
        }

        public List<UserRole> AllUserRoles { get; private set; }
        public string Title { get; private set; }

        public string Name
        {
            get { return Element.Name; }
            set
            {
                if (value == Element.Name) return;
                Element.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public UserRole UserRole
        {
            get { return Element.UserRole; }
            set
            {
                if (value == Element.UserRole)
                    return;
                Element.UserRole = value;
                NotifyOfPropertyChange(() => UserRole);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.User))) 
                return;

            EventAggregator.Publish(new UserChangedEvent(Element.User));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/User.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override UserModel CreateNewElementModel()
        {
            PreloadLists();
            return new UserModel(new User());
        }

        protected override UserModel CreateElementModel(int elementId)
        {
            UserModel model = null;
            DbConversation.UsingTransaction(() =>
                {
                    PreloadLists();
                    model = new UserModel(DbConversation.GetById<User>(elementId));
                });
            return model;
        }

        void PreloadLists()
        {
            AllUserRoles = DbConversation.Query(new AllUserRolesQuery()).ToList();
        }

        public void Handle(UserRoleChangedEvent message)
        {
            AllUserRoles = DbConversation.Query(new AllUserRolesQuery()).ToList();
        }

        public void Handle(UserRoleRemovedEvent message)
        {
            AllUserRoles = DbConversation.Query(new AllUserRolesQuery()).ToList();
        }
    }
}