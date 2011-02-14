using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Infrastructure
{
    public interface IWorkspaceCollector
    {
        ObservableCollection<WorkspaceViewModel> Workspaces { get; }
        void SetActiveWorkspace(WorkspaceViewModel workspace);
        TView FindView<TView>() where TView : WorkspaceViewModel;
        TView FindView<TView>(int entity) where TView : WorkspaceViewModel;
    }

    public class WorkspaceCollector : IWorkspaceCollector
    {
        ObservableCollection<WorkspaceViewModel> _workspaces;

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        public TView FindView<TView>() where TView : WorkspaceViewModel
        {
            var query = from w in Workspaces where w.GetType() == typeof (TView) select w;
            var view = query.FirstOrDefault();
            return view as TView;
        }

        public TView FindView<TView>(int entity) where TView : WorkspaceViewModel
        {
            if( !typeof(TView).IsSubclassOf(typeof(ResponsibleWorkspaceViewModel)))
                throw new InvalidOperationException("Searching wrong workspace type");
            var query = from w in Workspaces where w.GetType() == typeof (TView) && w.IsResponsibleFor(entity) select w;
            var view = query.FirstOrDefault();
            return view as TView;
        }

        public void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            if( !Workspaces.Contains(workspace))
                Workspaces.Add(workspace);

            var collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = sender as WorkspaceViewModel;
            if (workspace == null) return;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

    }
}