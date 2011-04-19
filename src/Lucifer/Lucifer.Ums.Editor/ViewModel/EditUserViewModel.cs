using System.ComponentModel;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class EditUserViewModel : EditItemViewModel<UserModel>, IDataErrorInfo
    {
        public EditUserViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditUserView_NewUser;
            Title = Strings.EditUserView_TitleNew;
            ToolTip = Strings.AllUsersView_New_ToolTip;
        }

        public EditUserViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(Strings.EditUserView_UserIs, Element.Name);
            ToolTip = Strings.AllUsersView_Edit_ToolTip;
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.User))) 
                return;

            EventAggregator.Publish(new UserChangedEvent { User = Element.User});
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/User.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override UserModel CreateNewElementModel()
        {
            return new UserModel(new User());
        }

        protected override UserModel CreateElementModel(int elementId)
        {
            UserModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new UserModel(DbConversation.GetById<User>(elementId));
                });
            return model;
        }
    }
}