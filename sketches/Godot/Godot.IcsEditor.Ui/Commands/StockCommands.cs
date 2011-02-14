using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddStockEvent : CompositePresentationEvent<object> { }
    public class EditStockEvent : CompositePresentationEvent<int> { }
    public class RemoveStockEvent : CompositePresentationEvent<int> { }
    public class BookFromStockEvent : CompositePresentationEvent<int> { }
    public class SendToStockEvent : CompositePresentationEvent<int> { }
    public class DepositStockEvent : CompositePresentationEvent<int> { }
    public class RemovalStockEvent : CompositePresentationEvent<int> { }

    public class StockCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;
        readonly SubscriptionToken _bookFromToken;
        readonly SubscriptionToken _sendToToken;
        readonly SubscriptionToken _depositToken;
        readonly SubscriptionToken _removalToken;

        public StockCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddStockEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditStockEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemoveStockEvent>().Subscribe(RemoveElement);
            _bookFromToken = EventAggregator.GetEvent<BookFromStockEvent>().Subscribe(BookFromStock);
            _sendToToken = EventAggregator.GetEvent<SendToStockEvent>().Subscribe(SendToStock);
            _depositToken = EventAggregator.GetEvent<DepositStockEvent>().Subscribe(DepositStock);
            _removalToken = EventAggregator.GetEvent<RemovalStockEvent>().Subscribe(RemovalStock);
        }

        public override int Priority
        {
            get { return 60; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllStocks,
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewStock,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllStocksViewModel>() ??
                            ViewActivator.Display<AllStocksViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditStockViewModel>(0) ??
                            ViewActivator.Display<EditStockViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditStockViewModel>(entityId) ??
                            ViewActivator.Display<EditStockViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: StockCommands, Löschen fehlt
        }

        void BookFromStock(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<StockItemBookingViewModel>() ??
                            ViewActivator.Display<StockItemBookingViewModel>();
            workspace.SetFromStock(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void SendToStock(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<StockItemBookingViewModel>() ??
                            ViewActivator.Display<StockItemBookingViewModel>();
            workspace.SetToStock(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void DepositStock(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<StockItemDepositViewModel>() ??
                            ViewActivator.Display<StockItemDepositViewModel>();
            workspace.SetStock(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void RemovalStock(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<StockItemRemovalViewModel>() ??
                            ViewActivator.Display<StockItemRemovalViewModel>();
            workspace.SetStock(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddStockEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditStockEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemoveStockEvent>().Unsubscribe(_removeElementToken);
            EventAggregator.GetEvent<BookFromStockEvent>().Unsubscribe(_bookFromToken);
            EventAggregator.GetEvent<SendToStockEvent>().Unsubscribe(_sendToToken);
            EventAggregator.GetEvent<DepositStockEvent>().Unsubscribe(_depositToken);
            EventAggregator.GetEvent<RemovalStockEvent>().Unsubscribe(_removalToken);
        }
    }
}