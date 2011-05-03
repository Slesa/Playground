using System.ComponentModel;
using System.Globalization;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditCurrencyViewModel : EditItemViewModel<CurrencyModel>, IDataErrorInfo
    {
        public EditCurrencyViewModel() 
        {
            DisplayName = Strings.EditCurrencyView_NewCurrency;
            Title = Strings.EditCurrencyView_TitleNew;
            ToolTip = Strings.AllCurrenciesView_New_ToolTip;
        }

        public EditCurrencyViewModel(int id)
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
        public string Contraction
        {
            get { return Element.Contraction; }
            set
            {
                if (value == Element.Contraction) return;
                Element.Contraction = value;
                NotifyOfPropertyChange(() => Contraction);
            }
        }
        public string Symbol
        {
            get { return Element.Symbol; }
            set
            {
                if (value == Element.Symbol) return;
                Element.Symbol = value;
                NotifyOfPropertyChange(() => Symbol);
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
        public int DecimalPosition
        {
            get { return Element.DecimalPosition; }
            set
            {
                if (value == Element.DecimalPosition) return;
                Element.DecimalPosition = value;
                NotifyOfPropertyChange(() => DecimalPosition);
            }
        }
        public char DecimalChar
        {
            get { return Element.DecimalChar; }
            set
            {
                if (value == Element.DecimalChar) return;
                Element.DecimalChar = value;
                NotifyOfPropertyChange(() => DecimalChar);
            }
        }
        public char ThousandChar
        {
            get { return Element.ThousandChar; }
            set
            {
                if (value == Element.ThousandChar) return;
                Element.ThousandChar = value;
                NotifyOfPropertyChange(() => ThousandChar);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Currency))) 
                return;

            EventAggregator.Publish(new CurrencyChangedEvent(Element.Currency));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Currency.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override CurrencyModel CreateNewElementModel()
        {
            return new CurrencyModel(new Currency());
        }

        protected override CurrencyModel CreateElementModel(int elementId)
        {
            CurrencyModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new CurrencyModel(DbConversation.GetById<Currency>(elementId));
                });
            return model;
        }
        
    }
}