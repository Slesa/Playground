using System.ComponentModel;
using System.Globalization;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditPurchaseFamilyViewModel : EditItemViewModel<PurchaseFamilyModel>, IDataErrorInfo
    {
        public EditPurchaseFamilyViewModel() 
        {
            DisplayName = Strings.EditPurchaseFamilyView_NewPurchaseFamily;
            Title = Strings.EditPurchaseFamilyView_TitleNew;
            ToolTip = Strings.AllPurchaseFamiliesView_New_ToolTip;
        }

        public EditPurchaseFamilyViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditPurchaseFamilyView_PurchaseFamilyIs, Element.Name);
            ToolTip = Strings.AllPurchaseFamiliesView_Edit_ToolTip;
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.PurchaseFamily))) 
                return;

            EventAggregator.Publish(new PurchaseFamilyChangedEvent(Element.PurchaseFamily));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/PurchaseFamily.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override PurchaseFamilyModel CreateNewElementModel()
        {
            return new PurchaseFamilyModel(new PurchaseFamily());
        }

        protected override PurchaseFamilyModel CreateElementModel(int elementId)
        {
            PurchaseFamilyModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new PurchaseFamilyModel(DbConversation.GetById<PurchaseFamily>(elementId));
                });
            return model;
        }
    }
}