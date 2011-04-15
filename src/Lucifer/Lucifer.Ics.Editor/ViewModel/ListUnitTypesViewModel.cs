using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel : SelectionListViewModel<UnitTypeRowViewModel>, IIcsModule, IHandle<UnitTypeChangedEvent>
    {
        public ListUnitTypesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.UnitTypesModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitTypeViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var unitType in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditUnitTypeViewModel(unitType.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selection = ElementList.Where(x => x.IsSelected).Take(10);
            if (selection.Count() == 0)
                return;

            var message = string.Format("Are you sure to remove the following unit types:\n");
            message = selection.Aggregate(
                message, (current, unitType) => current + string.Format("{0} {1}", unitType.Id, unitType.Name));

            if (MessageBox.Show(message, "Remove unit types", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            try
            {
                DbConversation.UsingTransaction(() => 
                {
                    foreach(var element in selection)
                    DbConversation.DeleteOnCommit(element.ElementData);
                });
            }
            catch(Exception exception)
            {
                MessageBox.Show("Error: unable to remove unit types\n{0}", exception.Message);
            }
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
                .Select(x => new UnitTypeRowViewModel(x)));
        }

        public void Handle(UnitTypeChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.UnitType.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new UnitTypeRowViewModel(message.UnitType);
                ElementList.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.UnitType);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }
    }
}
