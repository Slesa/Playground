using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;
using Lucifer.Ics.Model.Queries;

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
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditStockView_PurchaseStockIs, Element.Name);
            ToolTip = Strings.AllStocksView_Edit_ToolTip;
        }

        public List<RecipeableItem> AllRecipeableItems { get; private set; }
        public List<Unit> AllUnits { get; private set; }
        public ObservableCollection<StockItemRowViewModel> StockItems { get; private set; }
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

            EventAggregator.Publish(new StockChangedEvent(Element.Stock));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Stock.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        void PreloadLists(StockModel model)
        {
            AllRecipeableItems = DbConversation.Query(new AllRecipeableItemsQuery()).ToList();
            AllUnits = DbConversation.Query(new AllUnitsQuery()).ToList();
            StockItems = new ObservableCollection<StockItemRowViewModel>(
                model.StockItems
                .Select(x => new StockItemRowViewModel(x)));
            //StockItems.CollectionChanged += OnStockItemsChanged;
        }

        protected override StockModel CreateNewElementModel()
        {
            var model = new StockModel(new Stock());
            PreloadLists(model);
            return model;
        }

        protected override StockModel CreateElementModel(int elementId)
        {
            StockModel model = null;
            DbConversation.UsingTransaction(() =>
                {
                    model = new StockModel(DbConversation.GetById<Stock>(elementId));
                    PreloadLists(model);
                });
            return model;
        }
    }
}