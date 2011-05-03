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
    public class ListUnitsViewModel : SelectionListViewModel<UnitRowViewModel>, IIcsModule
        , IHandle<UnitChangedEvent>
        , IHandle<UnitRemovedEvent>
        , IHandle<UnitTypeChangedEvent>
    {
        public ListUnitsViewModel()
            : base(Strings.UnitsModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditUnitViewModel());
        }

        public void Edit()
        {
            foreach (var unit in ElementList.Where(unit => unit.IsSelected))
                ScreenManager.ActivateItem(new EditUnitViewModel(unit.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllUnitsView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, unit) => current + string.Format(CultureInfo.CurrentCulture, "  [ {0} ]", unit));

                var question = new QuestionViewModel(Strings.AllUnitsView_RemoveTitle, message,
                    Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new UnitRemovedEvent(t.Id));
                }
            }
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