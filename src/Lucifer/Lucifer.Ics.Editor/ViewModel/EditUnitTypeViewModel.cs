using System.ComponentModel;
using System.Globalization;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditUnitTypeViewModel : EditItemViewModel<UnitTypeModel>, IDataErrorInfo
    {
        public EditUnitTypeViewModel() 
        {
            DisplayName = Strings.EditUnitTypeView_NewUnitType;
            Title = Strings.EditUnitTypeView_TitleNew;
            ToolTip = Strings.AllUnitTypesView_New_ToolTip;
        }

        public EditUnitTypeViewModel(int id)
            : base(id)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditUnitTypeView_UnitTypeIs, Element.Name);
            ToolTip = Strings.AllUnitTypesView_Edit_ToolTip;
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
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.UnitType))) 
                return;

            EventAggregator.Publish(new UnitTypeChangedEvent(Element.UnitType));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/UnitType.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override UnitTypeModel CreateNewElementModel()
        {
            return new UnitTypeModel(new UnitType());
        }

        protected override UnitTypeModel CreateElementModel(int elementId)
        {
            UnitTypeModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new UnitTypeModel(DbConversation.GetById<UnitType>(elementId));
                });
            return model;
        }

    }
}