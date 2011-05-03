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
    public class ListSalesFamiliesViewModel : SelectionListViewModel<SalesFamilyRowViewModel>, IPmsModule
        , IHandle<SalesFamilyChangedEvent>
        , IHandle<SalesFamilyRemovedEvent>
    {
        public ListSalesFamiliesViewModel() 
            : base(Strings.SalesFamiliesModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            //_windowManager.ShowDialog(new EditUnitTypeViewModel());
            ScreenManager.ActivateItem(new EditSalesFamilyViewModel());
        }

        public void Edit()
        {
            foreach (var salesFamily in ElementList.Where(x => x.IsSelected))
                ScreenManager.ActivateItem(new EditSalesFamilyViewModel(salesFamily.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {
                var message = Strings.AllSalesFamiliesView_RemoveMessage;
                message = selectesForMessage.Aggregate( message,
                    (current, salesFamily) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", salesFamily.Id, salesFamily.Name));

                var question = new QuestionViewModel(Strings.AllSalesFamiliesView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new SalesFamilyRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.SalesFamiliesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/SalesFamily.png"; }
        }

        public string ToolTip
        {
            get { return Strings.SalesFamiliesTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<SalesFamilyRowViewModel> CreateElementList()
        {
            return new BindableCollection<SalesFamilyRowViewModel>(DbConversation
                .Query(new AllSalesFamiliesQuery())
                .Select(x => new SalesFamilyRowViewModel(x)));
        }

        public void Handle(SalesFamilyChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.SalesFamily.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SalesFamilyRowViewModel(message.SalesFamily);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.SalesFamily);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(SalesFamilyRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
    }
}