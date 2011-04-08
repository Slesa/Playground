using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel: Screen, IIcsModule
    {
        public ListUnitTypesViewModel()
        {
            DisplayName = Strings.UnitTypesModule;
            CreateAllUnitTypes();
        }

        public ObservableCollection<UnitTypeRowViewModel> AllUnitTypes { get; private set; }

        void CreateAllUnitTypes()
        {
            AllUnitTypes = new ObservableCollection<UnitTypeRowViewModel>
                {
                    new UnitTypeRowViewModel(new UnitType {Name = "Unit type 1"}),
                };
        }

        public void Add()
        {

        }

        public void Edit()
        {
        }

        public bool CanEdit()
        {
            return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; ;
        }

        public void Remove()
        {
        }

        public bool CanRemove()
        {
            return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; ;
        }

        public string ModuleName
        {
            get { return Strings.UnitTypesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/UnitType.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UnitTypesTooltip; }
        }
    }
}
