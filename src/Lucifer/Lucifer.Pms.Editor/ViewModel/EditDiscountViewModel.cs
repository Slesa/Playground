using System.ComponentModel;
using System.Globalization;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditDiscountViewModel : EditItemViewModel<DiscountModel>, IDataErrorInfo
    {
        public EditDiscountViewModel() 
        {
            DisplayName = Strings.EditCurrencyView_NewCurrency;
            Title = Strings.EditCurrencyView_TitleNew;
            ToolTip = Strings.AllCurrenciesView_New_ToolTip;
        }

        public EditDiscountViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditCurrencyView_CurrencyIs, Element.Name);
            ToolTip = Strings.AllCurrenciesView_Edit_ToolTip;
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
        public decimal Rate
        {
            get { return Element.Rate; }
            set
            {
                if (value == Element.Rate) return;
                Element.Rate = value;
                NotifyOfPropertyChange(() => Rate);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Discount))) 
                return;

            EventAggregator.Publish(new DiscountChangedEvent(Element.Discount));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Discount.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override DiscountModel CreateNewElementModel()
        {
            return new DiscountModel(new Discount());
        }

        protected override DiscountModel CreateElementModel(int elementId)
        {
            DiscountModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new DiscountModel(DbConversation.GetById<Discount>(elementId));
                });
            return model;
        }
    }
}