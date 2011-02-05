using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddUnitTypeEvent : CompositePresentationEvent<object> { }
    public class EditUnitTypeEvent : CompositePresentationEvent<int> { }
    public class RemoveUnitTypeEvent : CompositePresentationEvent<int> { }
    
    public class UnitTypeCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public UnitTypeCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddUnitTypeEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditUnitTypeEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemoveUnitTypeEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 10; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllUnitTypes,
                            new ActionCommand(param => ListElements())),
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllUnitTypesViewModel>() ??
                            ViewActivator.Display<AllUnitTypesViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditUnitTypeViewModel>(0) ??
                            ViewActivator.Display<EditUnitTypeViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditUnitTypeViewModel>(entityId) ??
                            ViewActivator.Display<EditUnitTypeViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: UnitTypeCommands, Löschen fehlt
        }

        public override void OnDispose()
        {
            EventAggregator.GetEvent<AddUnitTypeEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditUnitTypeEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemoveUnitTypeEvent>().Unsubscribe(_removeElementToken);
        }
    }
}