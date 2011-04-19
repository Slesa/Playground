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
            DisplayName = Strings.EditUserView_TitleNew;
            Title = Strings.EditUserView_NewUser; 
        }

        public EditUserViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
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

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/User.png"; }
        }

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