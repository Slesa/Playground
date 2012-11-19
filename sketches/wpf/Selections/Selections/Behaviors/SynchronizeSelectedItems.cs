using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Selections.Helpers;

namespace Selections.Behaviors
{
    /// <summary>
    /// Custom behavior that synchronizes the list in <see cref="ListBox.SelectedItems"/> with a collection.
    /// </summary>
    /// <remarks>
    /// This behavior uses a weak event handler to listen for changes on the synchronized collection.
    /// </remarks>
    public class SynchronizeSelectedItems : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectionsProperty =
            DependencyProperty.Register(
                "Selections",
                typeof(IList),
                typeof(SynchronizeSelectedItems),
                new PropertyMetadata(null, OnSelectionsPropertyChanged));

        bool _updating;
        WeakEventHandler<SynchronizeSelectedItems, object, NotifyCollectionChangedEventArgs> _currentWeakHandler;

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "Dependency property")]
        public IList Selections
        {
            get { return (IList)GetValue(SelectionsProperty); }
            set { SetValue(SelectionsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectedItemsChanged;
            UpdateSelectedItems();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectedItemsChanged;
            base.OnDetaching();
        }

        static void OnSelectionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as SynchronizeSelectedItems;

            if (behavior != null)
            {
                if (behavior._currentWeakHandler != null)
                {
                    behavior._currentWeakHandler.Detach();
                    behavior._currentWeakHandler = null;
                }
                if (e.NewValue != null)
                {
                    var notifyCollectionChanged = e.NewValue as INotifyCollectionChanged;
                    if (notifyCollectionChanged != null)
                    {
                        behavior._currentWeakHandler =
                            new WeakEventHandler<SynchronizeSelectedItems, object, NotifyCollectionChangedEventArgs>(
                                behavior,
                                (instance, sender, args) => instance.OnSelectionsCollectionChanged(sender, args),
                                listener => notifyCollectionChanged.CollectionChanged -= listener.OnEvent);
                        notifyCollectionChanged.CollectionChanged += behavior._currentWeakHandler.OnEvent;
                    }

                    behavior.UpdateSelectedItems();
                }
            }
        }

        void OnSelectedItemsChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelections(e);
        }

        void UpdateSelections(SelectionChangedEventArgs e)
        {
            ExecuteIfNotUpdating(
                () =>
                {
                    if (Selections != null)
                    {
                        foreach (var item in e.AddedItems)
                        {
                            Selections.Add(item);
                        }
                        foreach (var item in e.RemovedItems)
                        {
                            Selections.Remove(item);
                        }
                    }
                });
        }

        void OnSelectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedItems(e);
        }

        void UpdateSelectedItems()
        {
            ExecuteIfNotUpdating(
                () =>
                {
                    if (AssociatedObject != null)
                    {
                        AssociatedObject.SelectedItems.Clear();
                        foreach (var item in Selections ?? new object[0])
                        {
                            AssociatedObject.SelectedItems.Add(item);
                        }
                    }
                });
        }

        void UpdateSelectedItems(NotifyCollectionChangedEventArgs e)
        {
            ExecuteIfNotUpdating(
                () =>
                {
                    if (AssociatedObject != null)
                    {
                        if (e.Action == NotifyCollectionChangedAction.Reset)
                        {
                            AssociatedObject.SelectedItems.Clear();
                            return;
                        }
                        if (e.Action == NotifyCollectionChangedAction.Add)
                        {
                            foreach (var item in e.NewItems)
                            {
                                AssociatedObject.SelectedItems.Add(item);
                            }
                        }
                        if (e.Action == NotifyCollectionChangedAction.Remove)
                        {
                            foreach (var item in e.OldItems)
                            {
                                AssociatedObject.SelectedItems.Remove(item);
                            }
                        }
                    }
                });
        }

        void ExecuteIfNotUpdating(Action execute)
        {
            if (_updating) return;
            try
            {
                _updating = true;
                execute();
            }
            finally
            {
                _updating = false;
            }
        }
    }
}