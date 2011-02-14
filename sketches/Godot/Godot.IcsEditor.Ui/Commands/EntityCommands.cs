using System;
using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public abstract class EntityCommands : IDisposable, IEntityCommands
    {
        protected EntityCommands(IViewActivator viewViewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
        {
            ViewActivator = viewViewActivator;
            WorkspaceCollector = workspaceCollector;
            EventAggregator = eventAggregator;
        }

        public IEventAggregator EventAggregator { get; private set; }
        public IViewActivator ViewActivator { get; private set; }
        public IWorkspaceCollector WorkspaceCollector { get; private set; }

        public void Dispose()
        {
            OnDispose();
        }

        public abstract int Priority { get; }
        public abstract List<CommandViewModel> SupportedCommands { get; }

        public abstract void OnDispose();
    }
}