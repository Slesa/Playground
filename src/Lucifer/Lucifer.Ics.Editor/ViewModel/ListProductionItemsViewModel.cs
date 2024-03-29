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
    public class ListProductionItemsViewModel : SelectionListViewModel<ProductionItemRowViewModel>, IIcsModule
        , IHandle<ProductionItemChangedEvent>
        , IHandle<ProductionItemRemovedEvent>
    {
        public ListProductionItemsViewModel()
            : base(Strings.ProductionItemsModule)
        {
            EventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditProductionItemViewModel());
        }

        public void Edit()
        {
            foreach (var productionItem in ElementList.Where(pf => pf.IsSelected))
                ScreenManager.ActivateItem(new EditProductionItemViewModel(productionItem.Id));
        }

        public void Open(ProductionItemRowViewModel viewModel)
        {
            ScreenManager.ActivateItem(new EditProductionItemViewModel(viewModel.Id));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {

                var message = Strings.AllProductionItemsView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message, (current, pf) => current + string.Format(CultureInfo.CurrentCulture,"{0} {1}", pf.Id, pf.Name));

                var question = new QuestionViewModel(Strings.AllProductionItemsView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new ProductionItemRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.ProductionItemsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/ProductionItem.png"; }
        }

        public string ToolTip
        {
            get { return Strings.ProductionItemsTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<ProductionItemRowViewModel> CreateElementList()
        {
            return new BindableCollection<ProductionItemRowViewModel>(DbConversation
                .Query(new AllProductionItemsQuery())
                .Select(x => new ProductionItemRowViewModel(x)));
        }

        public void Handle(ProductionItemChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.ProductionItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new ProductionItemRowViewModel(message.ProductionItem);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.ProductionItem);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(ProductionItemRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
        
    }
}