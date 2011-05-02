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
    public class ListUnitsViewModel : SelectionListViewModel<UnitRowViewModel>, IIcsModule
        , IHandle<UnitChangedEvent>
        , IHandle<UnitRemovedEvent>
        , IHandle<UnitTypeChangedEvent>
    {
        public ListUnitsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.UnitsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var unit in ElementList.Where(unit => unit.IsSelected))
                ScreenManager.ActivateItem(new EditUnitViewModel(unit.Id, DbConversation, EventAggregator));
        }

        public void Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() == 0)
                return;

            var message = string.Format(Strings.AllUnitsView_RemoveMessage);
            message = selectesForMessage.Aggregate(
                message, (current, unit) => current + string.Format("{0} {1}", unit.Id, unit.Name));

            if (MessageBox.Show(message, Strings.AllUnitsView_RemoveTitle, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
            if (removedItems == null)
                return;

            foreach (var t in removedItems)
                EventAggregator.Publish(new UnitRemovedEvent(t.Id));
        }

        #region IIcsModule

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

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<UnitRowViewModel> CreateElementList()
        {
            return new BindableCollection<UnitRowViewModel>(DbConversation
                .Query(new AllUnitsQuery())
                .Select(x => new UnitRowViewModel(x)));
        }

        public void Handle(UnitChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Unit.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new UnitRowViewModel(message.Unit);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Unit);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(UnitRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }

        public void Handle(UnitTypeChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.UnitType==message.UnitType select vm);
            viewmodel.Each(x =>
                {
                    x.UnitType = message.UnitType;
                    x.Refresh();
                });
        }

    }
}