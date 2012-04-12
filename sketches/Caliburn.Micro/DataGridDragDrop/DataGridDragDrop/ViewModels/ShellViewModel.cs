using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using DataGridDragDrop.Data;
using DataGridDragDrop.Views;

namespace DataGridDragDrop.ViewModels 
{

    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        readonly GridItems _gridItems = new GridItems();

        public bool SingleSelect
        {
            get
            {
                var shellView = (ShellView)GetView();
                return shellView.DataItemsGrid.SelectionMode == DataGridSelectionMode.Single;
            }
        }

        protected override void OnActivate()
        {
            SetSelectModeButtonText();
            base.OnActivate();
        }

        public void SelectMode()
        {
            var shellView = (ShellView)GetView();
            shellView.DataItemsGrid.SelectionMode = SingleSelect ? DataGridSelectionMode.Extended : DataGridSelectionMode.Single;
            SetSelectModeButtonText();
        }

        void SetSelectModeButtonText()
        {
            var shellView = (ShellView)GetView();
            shellView.SelectMode.Content = SingleSelect ? "Single" : "Extended";
        }

        public void Save()
        {
            _gridItems.Save();
        }

        public void Close()
        {
            TryClose();
        }

        ObservableCollection<GridItemRowViewModel> _gridRows;
        public ObservableCollection<GridItemRowViewModel> GridRows
        {
            get
            {
                return _gridRows ??
                       (_gridRows = new ObservableCollection<GridItemRowViewModel>(
                           _gridItems.Items.Select(x => new GridItemRowViewModel(x))));
            }
        }

        #region DragAndDrop

        bool IsEditing { get; set; }

        public void OnBeginEdit()
        {
            IsEditing = true;
        }

        public void OnEndEdit()
        {
            IsEditing = false;
        }

        public void OnMouseDown(MouseButtonEventArgs e)
        {
            //if (IsEditing) return;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                return;

            var dataGrid = (DataGrid) e.Source;
            if (dataGrid == null) return;

            var cell = FindElementFromSource<DataGridCell>((DependencyObject) e.OriginalSource);
            if (cell == null) return;

            if (!cell.Column.IsReadOnly)
            {
                cell.Focus();
                dataGrid.BeginEdit();
                return;
            }

            if (!SingleSelect)
            {
                var draggedItems = new DraggedGridItems(); // { ViewModel = viewModel, Index = index };
                var selection = dataGrid.SelectedItems;
                if (selection.Count == 0) return;
                foreach (var element in selection)
                {
                    var viewModel = (GridItemRowViewModel) element;
                    var index = GridRows.IndexOf(viewModel);

                    //var draggedElement = new DraggedGridItem {ViewModel = viewModel, Index = index};
                    draggedItems.ViewModelIndices.Add(index, viewModel);
                }

                var dataObject = new DataObject(typeof(DraggedGridItems), draggedItems);
                DragDrop.DoDragDrop(dataGrid, dataObject, DragDropEffects.Move);
                e.Handled = true;
            }
            else
            {
                var viewModel = GetViewModelOfEvent((DependencyObject) e.OriginalSource);
                var index = GridRows.IndexOf(viewModel);

                var draggedElement = new DraggedGridItem {ViewModel = viewModel, Index = index};
                var dataObject = new DataObject(typeof (DraggedGridItem), draggedElement);
                DragDrop.DoDragDrop(dataGrid, dataObject, DragDropEffects.Move);
            }
        }

        public void OnDragEnter(DragEventArgs e)
        {
            if (!SingleSelect)
                WithDragItemsDo(e, di => WithRowFromSourceDo((DependencyObject) e.OriginalSource, row =>
                    {
                        di.OldBackground = row.Background;
                        row.Background = Brushes.Aquamarine;
                    }));
            else
                WithDragItemDo(e, di => WithRowFromSourceDo((DependencyObject) e.OriginalSource, row =>
                    {
                        di.OldBackground = row.Background;
                        row.Background = Brushes.Aquamarine;
                    }));
        }

        public void OnDragLeave(DragEventArgs e)
        {
            if (!SingleSelect)
                WithDragItemsDo(e, di => WithRowFromSourceDo((DependencyObject) e.OriginalSource, row =>
                    {
                        if (di.OldBackground != null)
                            row.Background = di.OldBackground;
                    }));
            else
                WithDragItemDo(e, di => WithRowFromSourceDo((DependencyObject) e.OriginalSource, row =>
                    {
                        if (di.OldBackground != null)
                            row.Background = di.OldBackground;
                    }));
        }

        public void OnDragDrop(DragEventArgs e)
        {
            OnDragLeave(e);
            if (!SingleSelect)
            {
                WithDragItemsDo(e, di => WithViewModelFromSourceDo((DependencyObject) e.OriginalSource, vm =>
                    {
                        var newIndex = GridRows.IndexOf(vm);

                        var inbetween = di.ViewModelIndices.Keys.Any(x => x == newIndex);
                        if (inbetween) return;

                        foreach (var keyValue in di.ViewModelIndices)
                        {
                            GridRows.RemoveAt(keyValue.Key);
                            GridRows.Insert(newIndex, keyValue.Value);
                            for (var i = 0; i < GridRows.Count; i++)
                                GridRows[i].Id = i;
                        }
                        var dataGrid = (DataGrid) e.Source;
                        if (dataGrid != null)
                        {
                            dataGrid.IsEnabled = false;
                            dataGrid.SelectedItems.Clear();
                            dataGrid.SelectedItem = null;
                            dataGrid.IsEnabled = true;
                        }
                    }));
                e.Handled = true;
            }
            else
                WithDragItemDo(e, di => WithViewModelFromSourceDo((DependencyObject)e.OriginalSource, vm =>
                    {
                        var newIndex = GridRows.IndexOf(vm);
                        if (newIndex == di.Index) return;

                        GridRows.RemoveAt(di.Index);
                        GridRows.Insert(newIndex, di.ViewModel);
                        for (var i = 0; i < GridRows.Count; i++)
                            GridRows[i].Id = i;
                    }));
        }

        static void WithViewModelFromSourceDo(DependencyObject source, Action<GridItemRowViewModel> action)
        {
            var vm = GetViewModelOfEvent(source);
            if (vm == null) return;

            action(vm);
        }

        static void WithDragItemDo(DragEventArgs e, Action<DraggedGridItem> action)
        {
            var dataObject = e.Data as DataObject;
            if (dataObject == null) return;

            var draggedItem = dataObject.GetData(typeof (DraggedGridItem)) as DraggedGridItem;
            if (draggedItem == null) return;

            action(draggedItem);
        }

        static void WithDragItemsDo(DragEventArgs e, Action<DraggedGridItems> action)
        {
            var dataObject = e.Data as DataObject;
            if (dataObject == null) return;

            var draggedItems = dataObject.GetData(typeof (DraggedGridItems)) as DraggedGridItems;
            if (draggedItems == null) return;

            action(draggedItems);
        }

        static void WithRowFromSourceDo(DependencyObject source, Action<DataGridRow> action)
        {
            var row = FindElementFromSource<DataGridRow>(source);
            if (row == null) return;

            action(row);
        }

        #endregion

        #region VisualTree

        static GridItemRowViewModel GetViewModelOfEvent(DependencyObject source)
        {
            var row = FindElementFromSource<DataGridRow>(source);
            if (row == null) return null;

            return row.DataContext as GridItemRowViewModel;
        }

        static T FindElementFromSource<T>(DependencyObject source) where T: UIElement
        {
            var dep = source;
            while ((dep != null) && !(dep is T))
                dep = VisualTreeHelper.GetParent(dep);
            return dep as T;
        }

        #endregion
    }

    public class DraggedGridItem
    {
        public GridItemRowViewModel ViewModel { get; set; }
        public int Index { get; set; }
        public Brush OldBackground { get; set; }
    }

    public class DraggedGridItems
    {
        Dictionary<int,GridItemRowViewModel> _viewModelindices;
        public Dictionary<int,GridItemRowViewModel> ViewModelIndices
        {
            get { return _viewModelindices ?? (_viewModelindices = new Dictionary<int, GridItemRowViewModel>()); }
        }

        public Brush OldBackground { get; set; }
    }
}
