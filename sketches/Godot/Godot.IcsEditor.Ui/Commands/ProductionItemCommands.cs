using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddProductionItemEvent : CompositePresentationEvent<object> { }
    public class EditProductionItemEvent : CompositePresentationEvent<int> { }
    public class RemoveProductionItemEvent : CompositePresentationEvent<int> { }

    public class ProductionItemCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public ProductionItemCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddProductionItemEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditProductionItemEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemoveProductionItemEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 50; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllProductionItems,
                            "/Godot.IcsEditor.Ui;component/Images/ProductionItem.png",
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewProductionItem,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllProductionItemsViewModel>() ??
                            ViewActivator.Display<AllProductionItemsViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditProductionItemViewModel>(0) ??
                            ViewActivator.Display<EditProductionItemViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditProductionItemViewModel>(entityId) ??
                            ViewActivator.Display<EditProductionItemViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: ProductionItemCommands, Löschen fehlt
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddProductionItemEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditProductionItemEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemoveProductionItemEvent>().Unsubscribe(_removeElementToken);
        }
    }
}