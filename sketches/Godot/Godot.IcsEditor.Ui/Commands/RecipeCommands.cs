using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddRecipeEvent : CompositePresentationEvent<object> { }
    public class EditRecipeEvent : CompositePresentationEvent<int> { }
    public class RemoveRecipeEvent : CompositePresentationEvent<int> { }

    public class RecipeCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public RecipeCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddRecipeEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditRecipeEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemoveRecipeEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 70; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllRecipes,
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewRecipe,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllRecipesViewModel>() ??
                            ViewActivator.Display<AllRecipesViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditRecipeViewModel>() ??
                            ViewActivator.Display<EditRecipeViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditRecipeViewModel>(entityId) ??
                            ViewActivator.Display<EditRecipeViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: RecipeCommands, Löschen fehlt
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddRecipeEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditRecipeEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemoveRecipeEvent>().Unsubscribe(_removeElementToken);
        }
    }
}