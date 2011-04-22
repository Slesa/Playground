using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class EditProductionItemViewModel : EditItemViewModel<ProductionItemModel>, IDataErrorInfo
    {
        public EditProductionItemViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditProductionItemView_NewProductionItem;
            Title = Strings.EditProductionItemView_TitleNew;
            ToolTip = Strings.AllProductionItemsView_New_ToolTip;
        }

        public EditProductionItemViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(Strings.EditProductionItemView_ProductionItemIs, Element.Name);
            ToolTip = Strings.AllProductionItemsView_Edit_ToolTip;
        }

        public List<RecipeableItem> AllRecipeableItems { get; private set; }
        public List<Unit> AllPurchaseUnits { get; private set; }
        public List<Unit> AllRecipeUnits { get; private set; }
        //public ObservableCollection<EditRecipeItemViewModel> RecipeItems { get; private set; }
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
        public Unit RecipeUnit
        {
            get { return Element.RecipeUnit; }
            set
            {
                if (value == Element.RecipeUnit) return;
                Element.RecipeUnit = value;
                NotifyOfPropertyChange(() => RecipeUnit);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.ProductionItem))) 
                return;

            EventAggregator.Publish(new ProductionItemChangedEvent { ProductionItem = Element.ProductionItem });
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/ProductionItem.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        void PreloadLists()
        {
            AllRecipeableItems = DbConversation.Query(new AllRecipeableItemsQuery()).ToList();
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();

            //RecipeItems = new ObservableCollection<SingleRecipeItemViewModel>(
            //    _editProductionItem.RecipeItems
            //    .Select(x => new SingleRecipeItemViewModel(x)));
            //RecipeItems.CollectionChanged += OnRecipeItemsChanged;
        }

        protected override ProductionItemModel CreateNewElementModel()
        {
            PreloadLists();
            return new ProductionItemModel(new ProductionItem());
        }

        protected override ProductionItemModel CreateElementModel(int elementId)
        {
            ProductionItemModel model = null;
            DbConversation.UsingTransaction(() =>
                {
                    PreloadLists();
                    model = new ProductionItemModel(DbConversation.GetById<ProductionItem>(elementId));
                });
            return model;
        }
    }
}