using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel : SelectionListViewModel<UnitTypeRowViewModel>, IIcsModule
    {
        readonly IWindowManager _windowManager;

        public ListUnitTypesViewModel(IDbConversation dbConversation, IWindowManager windowManager) 
            : base(Strings.UnitTypesModule, dbConversation)
        {
            _windowManager = windowManager;
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitTypeViewModel(DbConversation));
        }

        public void Edit()
        {
            foreach(var unitType in ElementList.Where(unitType => unitType.IsSelected) )
                ScreenManager.ActivateItem(new EditUnitTypeViewModel(unitType.Id, DbConversation));
        }

        public void Remove()
        {
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

        protected override ObservableCollection<UnitTypeRowViewModel> CreateElementList()
        {
            return new ObservableCollection<UnitTypeRowViewModel>(DbConversation
                .Query(new AllUnitTypesQuery())
                .Select(x=>new UnitTypeRowViewModel(x)));
        }
    }
}
