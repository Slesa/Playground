using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditPurchaseItemViewModel : EditItemViewModel<PurchaseItemModel>, IDataErrorInfo
        , IHandle<PurchaseFamilyChangedEvent>
        , IHandle<PurchaseFamilyRemovedEvent>
        , IHandle<UnitChangedEvent>
        , IHandle<UnitRemovedEvent>
    {
        public EditPurchaseItemViewModel() 
            : base()
        {
            DisplayName = Strings.EditPurchaseItemView_NewPurchaseItem;
            Title = Strings.EditPurchaseItemView_TitleNew;
            ToolTip = Strings.AllPurchaseItemsView_New_ToolTip;
        }

        public EditPurchaseItemViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditPurchaseItemView_PurchaseItemIs, Element.Name);
            ToolTip = Strings.AllPurchaseItemsView_Edit_ToolTip;
        }

        public List<PurchaseFamily> AllFamilies { get; private set; }
        public List<Unit> AllPurchaseUnits { get; private set; }
        public List<Unit> AllRecipeUnits { get; private set; }
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
        public PurchaseFamily PurchaseFamily
        {
            get { return Element.PurchaseFamily; }
            set
            {
                if (value == Element.PurchaseFamily)
                    return;
                Element.PurchaseFamily = value;
                NotifyOfPropertyChange(() => PurchaseFamily);
            }
        }
        public Unit PurchaseUnit
        {
            get { return Element.PurchaseUnit; }
            set
            {
                if (value == Element.PurchaseUnit)
                    return;
                Element.PurchaseUnit = value;
                NotifyOfPropertyChange(() => PurchaseUnit);
            }
        }
        public Unit RecipeUnit
        {
            get { return Element.RecipeUnit; }
            set
            {
                if (value == Element.RecipeUnit)
                    return;
                Element.RecipeUnit = value;
                NotifyOfPropertyChange(() => RecipeUnit);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.PurchaseItem))) 
                return;

            EventAggregator.Publish(new PurchaseItemChangedEvent(Element.PurchaseItem));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/PurchaseItem.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        void PreloadLists()
        {
            AllFamilies = DbConversation.Query(new AllPurchaseFamiliesQuery()).ToList();
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();
        }
        protected override PurchaseItemModel CreateNewElementModel()
        {
            PreloadLists();
            return new PurchaseItemModel(new PurchaseItem());
        }

        protected override PurchaseItemModel CreateElementModel(int elementId)
        {
            PurchaseItemModel model = null;
            DbConversation.UsingTransaction(() =>
                {
                    PreloadLists();
                    model =new PurchaseItemModel(DbConversation.GetById<PurchaseItem>(elementId));
                });
            return model;
        }

        public void Handle(PurchaseFamilyChangedEvent message)
        {
            AllFamilies = DbConversation.Query(new AllPurchaseFamiliesQuery()).ToList();
        }

        public void Handle(PurchaseFamilyRemovedEvent message)
        {
            AllFamilies = DbConversation.Query(new AllPurchaseFamiliesQuery()).ToList();
        }
        public void Handle(UnitChangedEvent message)
        {
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();
        }

        public void Handle(UnitRemovedEvent message)
        {
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();
        }
    }
}