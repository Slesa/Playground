using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class AddUnitEvent : CompositePresentationEvent<object> { }
    public class EditUnitEvent : CompositePresentationEvent<int> { }
    public class RemoveUnitEvent : CompositePresentationEvent<int> { }

    public class UnitCommands : EntityCommands
    {
        readonly SubscriptionToken _addElementToken;
        readonly SubscriptionToken _editElementToken;
        readonly SubscriptionToken _removeElementToken;

        public UnitCommands(IViewActivator viewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(viewActivator, workspaceCollector, eventAggregator)
        {
            _addElementToken = EventAggregator.GetEvent<AddUnitEvent>().Subscribe(o => AddElement());
            _editElementToken = EventAggregator.GetEvent<EditUnitEvent>().Subscribe(EditElement);
            _removeElementToken = EventAggregator.GetEvent<RemoveUnitEvent>().Subscribe(RemoveElement);
        }

        public override int Priority
        {
            get { return 20; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get
            {
                return new List<CommandViewModel>
                    {
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_ViewAllUnits,
                            new ActionCommand(param => ListElements())),
                        /*
                        new CommandViewModel(
                            Strings.ViewModel_MainWindowViewModel_CreateNewUnit,
                            new ActionCommand(param => AddElement())),
                         */
                    };
            }
        }

        void ListElements()
        {
            var workspace = WorkspaceCollector.FindView<AllUnitsViewModel>() ??
                            ViewActivator.Display<AllUnitsViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void AddElement()
        {
            var workspace = WorkspaceCollector.FindView<EditUnitViewModel>(0) ??
                            ViewActivator.Display<EditUnitViewModel>();
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        void EditElement(int entityId)
        {
            var workspace = WorkspaceCollector.FindView<EditUnitViewModel>(entityId) ??
                            ViewActivator.Display<EditUnitViewModel>(entityId);
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        static void RemoveElement(int entityId)
        {
            // TODO: UnitCommands, Löschen fehlt
        }

        override public void OnDispose()
        {
            EventAggregator.GetEvent<AddUnitEvent>().Unsubscribe(_addElementToken);
            EventAggregator.GetEvent<EditUnitEvent>().Unsubscribe(_editElementToken);
            EventAggregator.GetEvent<RemoveUnitEvent>().Unsubscribe(_removeElementToken);
        }
    }

}