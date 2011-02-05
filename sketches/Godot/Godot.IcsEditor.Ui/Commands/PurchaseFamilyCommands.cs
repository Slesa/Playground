using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddPurchaseFamilyEvent : CompositePresentationEvent<object> { }
    public class EditPurchaseFamilyEvent : CompositePresentationEvent<int> { }
    public class RemovePurchaseFamilyEvent : CompositePresentationEvent<int> { }

    public class PurchaseFamilyCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public PurchaseFamilyCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddPurchaseFamilyEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditPurchaseFamilyEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemovePurchaseFamilyEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 30; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllPurchaseFamilies,
                            "/Godot.IcsEditor.Ui;component/Images/PurchaseFamily.png",
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewPurchaseFamily,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllPurchaseFamiliesViewModel>() ??
                            ViewActivator.Display<AllPurchaseFamiliesViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditPurchaseFamilyViewModel>(0) ??
                            ViewActivator.Display<EditPurchaseFamilyViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditPurchaseFamilyViewModel>(entityId) ??
                            ViewActivator.Display<EditPurchaseFamilyViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: PurchaseFamilyCommands, Löschen fehlt
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddPurchaseFamilyEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditPurchaseFamilyEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemovePurchaseFamilyEvent>().Unsubscribe(_removeElementToken);
        }
        
    }
}