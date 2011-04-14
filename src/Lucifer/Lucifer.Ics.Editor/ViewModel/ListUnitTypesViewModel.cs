using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel: Screen, IIcsModule
    {
        readonly IDbConversation _dbConversation;
        readonly IWindowManager _windowManager;

        public ListUnitTypesViewModel(IDbConversation dbConversation, IWindowManager windowManager)
        {
            _dbConversation = dbConversation;
            _windowManager = windowManager;
            DisplayName = Strings.UnitTypesModule;
            CreateAllUnitTypes();
        }

        public ObservableCollection<UnitTypeRowViewModel> AllUnitTypes { get; private set; }

        void CreateAllUnitTypes()
        {
            AllUnitTypes = new ObservableCollection<UnitTypeRowViewModel>(_dbConversation
                .Query(new AllUnitTypesQuery())
                .Select(x=>new UnitTypeRowViewModel(x)));
            /* AllUnitTypes = new ObservableCollection<UnitTypeRowViewModel>
                {
                    new UnitTypeRowViewModel(new UnitType {Name = "Length"}),
                    new UnitTypeRowViewModel(new UnitType {Name = "Weight"}),
                    new UnitTypeRowViewModel(new UnitType {Name = "Area"}),
                    new UnitTypeRowViewModel(new UnitType {Name = "Pressure"}),
                };*/
        }

        public bool ItemSelected
        {
            get { return AllUnitTypes.Where(unitType => unitType.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; }
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitTypeViewModel(_dbConversation));
        }

        public void Edit()
        {
            foreach(var unitType in AllUnitTypes.Where(unitType => unitType.IsSelected) )
                ScreenManager.ActivateItem(new EditUnitTypeViewModel(unitType.Id, _dbConversation));
        }

        public bool CanEdit()
        {
            return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; 
        }

        public void Remove()
        {
        }

        public bool CanRemove()
        {
            return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; 
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

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

   }
}
