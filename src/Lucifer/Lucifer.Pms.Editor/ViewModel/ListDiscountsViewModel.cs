using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListDiscountsViewModel : SelectionListViewModel<DiscountRowViewModel>, IPmsModule
        , IHandle<DiscountChangedEvent>
        , IHandle<DiscountRemovedEvent>
    {
        public ListDiscountsViewModel() 
            : base(Strings.DiscountsModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditDiscountViewModel());
        }

        public void Edit()
        {
            foreach (var discount in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditDiscountViewModel(discount.Id));
        }

        public void Open(DiscountRowViewModel viewModel)
        {
            ScreenManager.ActivateItem(new EditDiscountViewModel(viewModel.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllDiscountsView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, discount) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", discount.Id, discount.Name));

                var question = new QuestionViewModel(Strings.AllDiscountsView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new DiscountRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.DiscountsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Discount.png"; }
        }

        public string ToolTip
        {
            get { return Strings.DiscountsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<DiscountRowViewModel> CreateElementList()
        {
            return new BindableCollection<DiscountRowViewModel>(DbConversation
                .Query(new AllDiscountsQuery())
                .Select(x => new DiscountRowViewModel(x)));
        }

        public void Handle(DiscountChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Discount.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new DiscountRowViewModel(message.Discount);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Discount);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(DiscountRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}