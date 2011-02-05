using System;
using System.Collections.Generic;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public class CrystalReportPrintEventArgs : EventArgs
    {
        public CrystalReportPrintEventArgs(string reportFile, string description)
        {
            ReportFile = reportFile;
            Description = description;
        }

        public string ReportFile { get; private set; }
        public string Description { get; private set; }
    }

    public class CrystalReportPrintEvent : CompositePresentationEvent<CrystalReportPrintEventArgs> { }

    public class CrystalReportCommands : EntityCommands
    {
        readonly SubscriptionToken _printReportToken;

        public CrystalReportCommands(IViewActivator viewViewActivator, IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator) : base(viewViewActivator, workspaceCollector, eventAggregator)
        {
            _printReportToken = EventAggregator.GetEvent<CrystalReportPrintEvent>().Subscribe(PrintReport);
        }

        void PrintReport(CrystalReportPrintEventArgs args)
        {
            var workspace = //WorkspaceCollector.FindView<CrystalReportViewModel>() ??
                            ViewActivator.Display<CrystalReportViewModel>(
                                new { reportFile = args.ReportFile, description = args.Description } );
            WorkspaceCollector.SetActiveWorkspace(workspace);
        }

        public override int Priority
        {
            get { return 0; }
        }

        public override List<CommandViewModel> SupportedCommands
        {
            get { return null; }
        }

        public override void OnDispose()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Unsubscribe(_printReportToken);
        }


    }
}