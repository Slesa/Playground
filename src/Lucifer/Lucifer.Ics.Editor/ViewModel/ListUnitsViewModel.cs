using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitsViewModel : SelectionListViewModel<UnitRowViewModel>, IIcsModule
    {
        public ListUnitsViewModel(IDbConversation dbConversation)
            : base(Strings.UnitsModule, dbConversation)
        {
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitViewModel());
        }

        public void Edit()
        {
            foreach (var unit in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditUnitTypeViewModel(unit.Id, DbConversation));
        }

        public void Remove()
        {
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

        protected override ObservableCollection<UnitRowViewModel> CreateElementList()
        {
            return new ObservableCollection<UnitRowViewModel>(DbConversation
                .Query(new AllUnitsQuery())
                .Select(x => new UnitRowViewModel(x)));
        }
    }
}