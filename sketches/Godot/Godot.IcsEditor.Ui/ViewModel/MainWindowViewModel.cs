using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Godot.IcsEditor.Ui.Commands;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.Localization;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        readonly IWorkspaceCollector _workspaceCollector;

        public MainWindowViewModel(IWorkspaceCollector workspaceCollector, IEventAggregator eventAggregator)
            : base(null, eventAggregator)
        {
            _workspaceCollector = workspaceCollector;
            base.DisplayName = Strings.ViewModel_MainWindowViewModel_DisplayName;
        }

        public IEntityCommands[] EntityCommands { get; set; }

        #region Commands

        ReadOnlyCollection<CommandViewModel> _commands;

        /// <summary>
        /// Returns a read-only list of commands that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    var cmds = CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            var commands = new List<CommandViewModel>();
            EntityCommands.OrderByDescending(cmd => cmd.Priority).Each(cmd =>
                {
                    var cmdList = cmd.SupportedCommands; if (cmdList != null) commands.AddRange(cmdList);
                });
            return commands;
        }

        #endregion // Commands

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get { return _workspaceCollector.Workspaces; }
        }

#if NEVER



        #region Stocks

/*
        void ShowMainBearing(object sender, EventArgs e)
        {
            var workspaces = Workspaces.Where(vm => vm is SingleStockViewModel);
            var query = from ws in workspaces where (ws as SingleStockViewModel).UnderlyingObject == null  select ws;
            var workspace = query.FirstOrDefault() as SingleStockViewModel;
            if (workspace == null)
            {
                workspace = new SingleStockViewModel(null, App.StockRepository, App.ProductionItemRepository,
                                                     App.UnitRepository);
                workspace.StockBookFrom += BookItemsIntoStock;
                workspace.StockSendTo += BookItemsIntoStock;
                workspace.StockItemsPrint += PrintCrystalReports;
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }
*/
/*
        void BookItemsIntoStock(object sender, StockItemBookingEventArgs e)
        {
            var workspace = new StockItemBookingViewModel(App.StockRepository, App.StockItemRepository, App.UnitRepository);
            if( e.FromStock!=null )
                workspace.FromStock = e.FromStock;
            if( e.ToStock!=null )
                workspace.ToStock = e.ToStock;
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }
*/
        #endregion // Stocks
#endif

    }

}