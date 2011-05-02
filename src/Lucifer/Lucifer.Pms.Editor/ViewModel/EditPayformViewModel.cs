using System.ComponentModel;
using System.Globalization;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditPayformViewModel : EditItemViewModel<PayformModel>, IDataErrorInfo
    {
        public EditPayformViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditPayformView_NewPayform;
            Title = Strings.EditPayformView_TitleNew;
            ToolTip = Strings.AllPayformsView_New_ToolTip;
        }

        public EditPayformViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditPayformView_PayformIs, Element.Name);
            ToolTip = Strings.AllPayformsView_Edit_ToolTip;
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Payform))) 
                return;

            EventAggregator.Publish(new PayformChangedEvent(Element.Payform));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Payform.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override PayformModel CreateNewElementModel()
        {
            return new PayformModel(new Payform());
        }

        protected override PayformModel CreateElementModel(int elementId)
        {
            PayformModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new PayformModel(DbConversation.GetById<Payform>(elementId));
                });
            return model;
        }
    }
}