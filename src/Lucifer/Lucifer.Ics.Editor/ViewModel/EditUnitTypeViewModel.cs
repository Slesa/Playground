using System.ComponentModel;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditUnitTypeViewModel : EditItemViewModel<UnitTypeModel>, IDataErrorInfo
    {

        public EditUnitTypeViewModel(IDbConversation dbConversation) 
            : base(Strings.EditUnitTypeView_TitleNew, dbConversation)
        {
            Title =Strings.EditUnitTypeView_NewUnitType; 
        }

        public EditUnitTypeViewModel(int id, IDbConversation dbConversation)
            : base(id, Strings.EditUnitTypeView_TitleNew, dbConversation)
        {
        }

        public string Title { get; private set; }

        public string Name
        {
            get { return _element.Name; }
            set
            {
                if (value == _element.Name) return;
                _element.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void Save()
        {

        }

 
        #region Module information

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/UnitType.png"; }
        }

        #endregion

        protected override UnitTypeModel CreateNewElementModel()
        {
            return new UnitTypeModel(new UnitType());
        }

        protected override UnitTypeModel CreateElementModel(int elementId)
        {
            UnitTypeModel model = null;
            _dbConversation.UsingTransaction(() =>
                { model =new UnitTypeModel(_dbConversation.GetById<UnitType>(elementId));
                });
            return model;
        }

    }
}