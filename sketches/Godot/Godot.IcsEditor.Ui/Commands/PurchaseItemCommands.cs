using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddPurchaseItemEvent : CompositePresentationEvent<object> { }
    public class EditPurchaseItemEvent : CompositePresentationEvent<int> { }
    public class RemovePurchaseItemEvent : CompositePresentationEvent<int> { }

    public class PurchaseItemCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public PurchaseItemCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddPurchaseItemEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditPurchaseItemEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemovePurchaseItemEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 40; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllPurchaseItems,
                            "/Godot.IcsEditor.Ui;component/Images/PurchaseItem.png",
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewPurchaseItem,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllPurchaseItemsViewModel>() ??
                            ViewActivator.Display<AllPurchaseItemsViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditPurchaseItemViewModel>(0) ??
                            ViewActivator.Display<EditPurchaseItemViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditPurchaseItemViewModel>(entityId) ??
                            ViewActivator.Display<EditPurchaseItemViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: PurchaseItemCommands, Löschen fehlt
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddPurchaseItemEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditPurchaseItemEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemovePurchaseItemEvent>().Unsubscribe(_removeElementToken);
        }
        
    }
}