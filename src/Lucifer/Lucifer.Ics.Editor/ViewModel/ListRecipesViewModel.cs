using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Editor.Results;
using Lucifer.Editor.ViewModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Queries;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListRecipesViewModel: SelectionListViewModel<RecipeRowViewModel>, IIcsModule
        , IHandle<RecipeChangedEvent>
        , IHandle<RecipeRemovedEvent>
    {
        public ListRecipesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(Strings.ProductionItemsModule, dbConversation, eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void Add()
        {
            ScreenManager.ActivateItem(new EditRecipeViewModel(DbConversation, EventAggregator));
        }

        public void Edit()
        {
            foreach (var recipe in ElementList.Where(pf => pf.IsSelected))
                ScreenManager.ActivateItem(new EditRecipeViewModel(recipe.Id, DbConversation, EventAggregator));
        }

        public IEnumerable<IResult> Remove()
        {
            var selectesForMessage = ElementList.Where(x => x.IsSelected).Take(10);
            if (selectesForMessage.Count() > 0)
            {

                var message = Strings.AllRecipesView_RemoveMessage;
                message = selectesForMessage.Aggregate(
                    message,
                    (current, pf) => current + string.Format(CultureInfo.CurrentCulture, "{0} {1}", pf.Id, pf.Plu));

                var question = new QuestionViewModel(Strings.AllRecipesView_RemoveTitle, message,
                                                     Answer.Yes, Answer.No);
                yield return new QuestionResult(question)
                    .CancelOn(Answer.No);

                var removedItems = RemoveSelectionWith(element => DbConversation.DeleteOnCommit(element.ElementData));
                if (removedItems != null)
                {
                    foreach (var t in removedItems)
                        EventAggregator.Publish(new RecipeRemovedEvent(t.Id));
                }
            }
        }

        #region IIcsModule

        public string ModuleName
        {
            get { return Strings.RecipesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Recipe.png"; }
        }

        public string ToolTip
        {
            get { return Strings.RecipesTooltip; }
        }

        #endregion

        public Conductor<IScreen>.Collection.OneActive ScreenManager
        {
            get;
            set;
        }

        protected override BindableCollection<RecipeRowViewModel> CreateElementList()
        {
            return new BindableCollection<RecipeRowViewModel>(DbConversation
                .Query(new AllRecipesQuery())
                .Select(x => new RecipeRowViewModel(x)));
        }

        public void Handle(RecipeChangedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Recipe.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new RecipeRowViewModel(message.Recipe);
                ElementList.Add(viewmodel);
                ConnectElement(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(message.Recipe);
                viewmodel.Refresh();
            }
            NotifyOfPropertyChange(() => ItemSelected);
            NotifyOfPropertyChange(() => ItemsSelected);
        }

        public void Handle(RecipeRemovedEvent message)
        {
            var viewmodel = (from vm in ElementList where vm.Id == message.Id select vm).FirstOrDefault();
            if (viewmodel != null)
                ElementList.Remove(viewmodel);
        }
        
    }
}