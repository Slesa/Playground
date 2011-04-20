using System.Collections.Generic;
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
    public class EditUnitViewModel : EditItemViewModel<UnitModel>, IDataErrorInfo
        , IHandle<UnitTypeChangedEvent>
        , IHandle<UnitTypeRemovedEvent>
    {
        public EditUnitViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditUnitView_NewUnit;
            Title = Strings.EditUnitView_TitleNew;
            ToolTip = Strings.AllUnitsView_New_ToolTip;
        }

        public EditUnitViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(Strings.EditUnitView_UnitIs, Element.Name);
            ToolTip = Strings.AllUnitsView_Edit_ToolTip;
        }

        public List<Unit> AllUnits { get; private set; }
        public List<UnitType> AllUnitTypes { get; private set; }
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
        public UnitType UnitType
        {
            get { return Element.UnitType; }
            set
            {
                if (value == Element.UnitType)
                    return;
                Element.UnitType = value;
                NotifyOfPropertyChange(() => ParentUnit);
                NotifyOfPropertyChange(() => UnitType);
            }
        }
        public Unit ParentUnit
        {
            get { return Element.Parent; }
            set
            {
                if (value == Element.Parent)
                    return;

                //if (_unitModel.Parent != null)
                //    _unitModel.Parent.RemoveChild(_unitModel);

                Element.Parent = value.Id == 0 ? null : value;

                //if (_unitModel.Parent != null)
                //    _unitModel.Parent.AddChild(_unitModel.Unit);

                NotifyOfPropertyChange(() => ParentUnit);
                NotifyOfPropertyChange(() => UnitType);
            }
        }
        public string FactorToParent
        {
            get { return Element.FactorToParent; }
            set
            {
                if (value == Element.FactorToParent)
                    return;
                Element.FactorToParent = value;
                NotifyOfPropertyChange(() => FactorToParent);
            }
        }
        public bool Purchasing
        {
            get { return Element.Purchasing; }
            set
            {
                if (value == Element.Purchasing)
                    return;
                Element.Purchasing = value;
                NotifyOfPropertyChange(() => Purchasing);
            }
        }
        public bool Reciping
        {
            get { return Element.Reciping; }
            set
            {
                if (value == Element.Reciping)
                    return;
                Element.Reciping = value;
                NotifyOfPropertyChange(() => Reciping);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Unit)))
                return;

            EventAggregator.Publish(new UnitChangedEvent { Unit = Element.Unit });
            TryClose();
        }


        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Unit.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override UnitModel CreateNewElementModel()
        {
            PreloadLists();
            return new UnitModel(new Unit());
        }

        protected override UnitModel CreateElementModel(int elementId)
        {
            UnitModel model = null;
            DbConversation.UsingTransaction(() =>
            {
                PreloadLists();
                model = new UnitModel(DbConversation.GetById<Unit>(elementId));
            });
            return model;
        }

        void PreloadLists()
        {
            AllUnits = DbConversation.Query(new AllUnitsQuery()).ToList();
            AllUnitTypes = DbConversation.Query(new AllUnitTypesQuery()).ToList();
        }

        public void Handle(UnitTypeChangedEvent message)
        {
            AllUnitTypes = DbConversation.Query(new AllUnitTypesQuery()).ToList();
            //var viewmodel = (from unit in AllUnits where unit.UnitType == message.UnitType select unit);
            //foreach(var vm in viewmodel)
            //    vm.UnitType = message.UnitType;
        }

        public void Handle(UnitTypeRemovedEvent message)
        {
            AllUnitTypes = DbConversation.Query(new AllUnitTypesQuery()).ToList();
        }
    }
}