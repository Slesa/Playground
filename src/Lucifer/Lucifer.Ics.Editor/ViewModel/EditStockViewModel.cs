using System.ComponentModel;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditStockViewModel : EditItemViewModel<StockModel>, IDataErrorInfo
    {
        public EditStockViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditStockView_NewStock;
            Title = Strings.EditStockView_TitleNew;
            ToolTip = Strings.AllStocksView_New_ToolTip;
        }

        public EditStockViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(Strings.EditStockView_PurchaseStockIs, Element.Name);
            ToolTip = Strings.AllStocksView_Edit_ToolTip;
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
        public bool IsMainStock
        {
            get { return Element.IsMainStock; }
            set
            {
                if (value == Element.IsMainStock) return;
                Element.IsMainStock = value;
                NotifyOfPropertyChange(() => IsMainStock);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Stock))) 
                return;

            EventAggregator.Publish(new StockChangedEvent { Stock = Element.Stock});
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Stock.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override StockModel CreateNewElementModel()
        {
            return new StockModel(new Stock());
        }

        protected override StockModel CreateElementModel(int elementId)
        {
            StockModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new StockModel(DbConversation.GetById<Stock>(elementId));
                });
            return model;
        }
    }
}