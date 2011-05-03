using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel : SelectionListViewModel<UnitTypeRowViewModel>, IIcsModule
        , IHandle<UnitTypeChangedEvent>
        , IHandle<UnitTypeRemovedEvent>
    {
        public ListUnitTypesViewModel()
            : base(Strings.UnitTypesModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitTypeViewModel());
        }

        public void Edit()
        {
            foreach (var unitType in ElementList.Where(unitType => unitType.IsSelected))
                ScreenManager.ActivateItem(new EditUnitTypeViewModel(unitType.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {

                var message = Strings.AllUnitTypesView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, unitType) => current 
                        + string.Format(CultureInfo.CurrentCulture, "  [ {0} {1} ]", unitType.Id, unitType.Name));

                var question = new QuestionViewModel(Strings.AllUnitTypesView_RemoveTitle, message, 
                    Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new UnitTypeRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

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

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<UnitTypeRowViewModel> CreateElementList()
        {
            return new BindableCollection<UnitTypeRowViewModel>(DbConversation
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
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.UnitType);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(UnitTypeRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}
