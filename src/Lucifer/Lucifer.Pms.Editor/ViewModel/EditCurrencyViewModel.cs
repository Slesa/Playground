using System.ComponentModel;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditCurrencyViewModel : EditItemViewModel<CurrencyModel>, IDataErrorInfo
    {
        public EditCurrencyViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditCurrencyView_TitleNew;
            Title = Strings.EditCurrencyView_NewCurrency; 
        }

        public EditCurrencyViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Currency))) 
                return;

            EventAggregator.Publish(new CurrencyChangedEvent { Currency = Element.Currency});
            TryClose();
        }

 
        #region Module information

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Currency.png"; }
        }

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