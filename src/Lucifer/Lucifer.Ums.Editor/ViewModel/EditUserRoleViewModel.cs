using System.ComponentModel;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class EditUserRoleViewModel : EditItemViewModel<UserRoleModel>, IDataErrorInfo
    {
        public EditUserRoleViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditUserRoleView_NewUserRole;
            Title = Strings.EditUserRoleView_TitleNew;
            ToolTip = Strings.AllUserRolesView_New_ToolTip;
        }

        public EditUserRoleViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(Strings.EditUserRoleView_UserRoleIs, Element.Name);
            ToolTip = Strings.AllUserRolesView_Edit_ToolTip;
        }

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

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.UserRole))) 
                return;

            EventAggregator.Publish(new UserRoleChangedEvent(Element.UserRole));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/UserRole.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override UserRoleModel CreateNewElementModel()
        {
            return new UserRoleModel(new UserRole());
        }

        protected override UserRoleModel CreateElementModel(int elementId)
        {
            UserRoleModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new UserRoleModel(DbConversation.GetById<UserRole>(elementId));
                });
            return model;
        }
    }
}