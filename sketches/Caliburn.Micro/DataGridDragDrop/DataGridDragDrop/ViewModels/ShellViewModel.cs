using System;
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
        public bool SingleSelect { get; set; }

        protected override void OnActivate()
        {
            SingleSelect = true;
            SelectMode();
            base.OnActivate();
        }

        public void SelectMode()
        {
            var shellView = (ShellView) GetView();
            if (SingleSelect)
            {
                SingleSelect = false;
                shellView.SelectMode.Content = "Single";
                shellView.DataItemsGrid.SelectionMode = DataGridSelectionMode.Single;
            }
            else
            {
                SingleSelect = true;
                shellView.SelectMode.Content = "Extended";
                shellView.DataItemsGrid.SelectionMode = DataGridSelectionMode.Extended;
            }
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

            var viewModel = GetViewModelOfEvent((DependencyObject) e.OriginalSource);
            var index = GridRows.IndexOf(viewModel);

            var draggedElement = new DraggedGridItem {ViewModel = viewModel, Index = index};
            var dataObject = new DataObject(typeof (DraggedGridItem), draggedElement);
            DragDrop.DoDragDrop(dataGrid, dataObject, DragDropEffects.Move);
        }

        public void OnDragEnter(DragEventArgs e)
        {
            WithDragItemDo(e, di=> WithRowFromSourceDo((DependencyObject)e.OriginalSource, row=>
                {
                    di.OldBackground = row.Background;
                    row.Background = Brushes.Aquamarine;
                }));
        }

        public void OnDragLeave(DragEventArgs e)
        {
            WithDragItemDo(e, di => WithRowFromSourceDo((DependencyObject)e.OriginalSource, row =>
                {
                    if (di.OldBackground != null)
                        row.Background = di.OldBackground;
                }));
        }

        public void OnDragDrop(DragEventArgs e)
        {
            OnDragLeave(e);
            WithDragItemDo(e, di=> WithViewModelFromSourceDo((DependencyObject)e.OriginalSource, vm =>
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
}
