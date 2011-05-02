using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class EditSalesItemViewModel : EditItemViewModel<SalesItemModel>, IDataErrorInfo
    {
        public EditSalesItemViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditSalesItemView_NewSalesItem;
            Title = Strings.EditSalesItemView_TitleNew;
            ToolTip = Strings.AllSalesItemsView_New_ToolTip;
        }

        public EditSalesItemViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditSalesItemView_SalesItemIs, Element.Name);
            ToolTip = Strings.AllSalesItemsView_Edit_ToolTip;
        }

        public List<SalesFamily> AllSalesFamilies { get; private set; }
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

        public SalesFamily SalesFamily
        {
            get { return Element.SalesFamily; }
            set
            {
                if (value == Element.SalesFamily) return;
                Element.SalesFamily = value;
                NotifyOfPropertyChange(() => SalesFamily);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.SalesItem))) 
                return;

            EventAggregator.Publish(new SalesItemChangedEvent(Element.SalesItem));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/SalesItem.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        void PreloadLists()
        {
            AllSalesFamilies = DbConversation.Query(new AllSalesFamiliesQuery()).ToList();
        }

        protected override SalesItemModel CreateNewElementModel()
        {
            PreloadLists();
            return new SalesItemModel(new SalesItem());
        }

        protected override SalesItemModel CreateElementModel(int elementId)
        {
            SalesItemModel model = null;
            DbConversation.UsingTransaction(() =>
                {
                    PreloadLists();
                    model = new SalesItemModel(DbConversation.GetById<SalesItem>(elementId));
                });
            return model;
        }
    }
}