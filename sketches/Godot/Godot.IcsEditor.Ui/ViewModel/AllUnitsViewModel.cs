using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Commands;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class AllUnitsViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\UnitList.rpt";

        private readonly SubscriptionToken _unitInsertedToken;

        public AllUnitsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllUnitsViewModel_DisplayName;

            CreateAllUnits();
            _unitInsertedToken = EventAggregator.GetEvent<UnitChangedEvent>().Subscribe(OnUnitChanged);
        }

        public ObservableCollection<SingleUnitViewModel> AllUnits { get; private set; }

        void CreateAllUnits()
        {
            AllUnits = new ObservableCollection<SingleUnitViewModel>(DbConversation
                .Query(new AllUnitsQuery())
                .Select(x => new SingleUnitViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllUnits.Where(unit => unit.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllUnits.FirstOrDefault(unit => unit.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<UnitChangedEvent>().Unsubscribe(_unitInsertedToken);
        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewUnit())); }
        }

        void NewUnit()
        {
            EventAggregator.GetEvent<AddUnitEvent>().Publish(null);
        }

        public ICommand DoubleClickCommand
        {
            get { return EditCommand; }
        }

        ActionCommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditUnit(), param => ItemsSelected));
            }
        }

        void EditUnit()
        {
            AllUnits.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditUnitEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemoveUnit(), param => ItemsSelected));
            }
        }

        void RemoveUnit()
        {
            var toDelete = AllUnits.Where(vm => vm.IsSelected).ToList();
            var msg = toDelete.Aggregate(Strings.ViewModel_AllUnitsViewModel_AskToDelete, (current, item) => current + ("\n" + item.DisplayName));
            if (MessageBox.Show(msg, Strings.ViewModel_AllUnitsViewModel_DeleteItems, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            try
            {
                DbConversation.UsingTransaction(() =>
                {
                    foreach (var instance in toDelete)
                        DbConversation.Delete(instance.UnderlayingObject());
                });
            }
            catch
            {
                MessageBox.Show(Strings.Error_Units_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllUnits.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintUnits(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintUnits()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllUnitsViewModel_PrintHeader));
        }

        #endregion

        void OnUnitChanged(Unit unit)
        {
            var viewmodel = (from vm in AllUnits where vm.Id == unit.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SingleUnitViewModel(unit);
                AllUnits.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(unit);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        /*
        void OnUnitRemoved(object sender, EntityRemovedEventArgs<Unit> e)
        {
            var viewModel = AllUnits.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
            AllUnits.Remove(viewModel);
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleUnitViewModel unitvm in e.NewItems)
                    unitvm.PropertyChanged += OnUnitViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleUnitViewModel unitvm in e.OldItems)
                    unitvm.PropertyChanged -= OnUnitViewModelPropertyChanged;
        }

        void OnUnitViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            const string isSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            var vm = (sender as SingleUnitViewModel);
            if (vm != null)
                vm.VerifyPropertyName(isSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            if (e.PropertyName == isSelected)
            {
                //this.OnPropertyChanged("TotalSelectedSales");
                OnPropertyChanged("ItemSelected");
                OnPropertyChanged("ItemsSelected");
            }
        }*/
    }
}