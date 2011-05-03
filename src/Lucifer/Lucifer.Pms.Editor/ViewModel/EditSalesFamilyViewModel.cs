using System.ComponentModel;
using System.Globalization;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditSalesFamilyViewModel : EditItemViewModel<SalesFamilyModel>, IDataErrorInfo
    {
        public EditSalesFamilyViewModel() 
        {
            DisplayName = Strings.EditSalesFamilyView_NewSalesFamily;
            Title = Strings.EditSalesFamilyView_TitleNew;
            ToolTip = Strings.AllSalesFamiliesView_New_ToolTip;
        }

        public EditSalesFamilyViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditSalesFamilyView_SalesFamilyIs, Element.Name);
            ToolTip = Strings.AllSalesFamiliesView_Edit_ToolTip;
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.SalesFamily))) 
                return;

            EventAggregator.Publish(new SalesFamilyChangedEvent(Element.SalesFamily));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/SalesFamily.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override SalesFamilyModel CreateNewElementModel()
        {
            return new SalesFamilyModel(new SalesFamily());
        }

        protected override SalesFamilyModel CreateElementModel(int elementId)
        {
            SalesFamilyModel model = null;
            DbConversation.UsingTransaction(() =>
            {
                model = new SalesFamilyModel(DbConversation.GetById<SalesFamily>(elementId));
                });
            return model;
        }
    }
}