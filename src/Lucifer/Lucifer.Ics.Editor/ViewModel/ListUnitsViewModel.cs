using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitsViewModel: Screen, IIcsModule
    {
        public ListUnitsViewModel()
        {
            DisplayName = Strings.UnitsModule;
            CreateAllUnits();
        }

        public ObservableCollection<UnitRowViewModel> AllUnits { get; private set; }

        void CreateAllUnits()
        {
            AllUnits = new ObservableCollection<UnitRowViewModel>
                {
                    new UnitRowViewModel(new Unit {Name = "Unit 1"}),
                };
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitViewModel());
        }

        public string ModuleName
        {
            get { return Strings.UnitsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Unit.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UnitsTooltip; }
        }

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }
    }
}