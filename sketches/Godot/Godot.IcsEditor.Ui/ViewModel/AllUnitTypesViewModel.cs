using System.Collections.Generic;
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
    public class AllUnitTypesViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\UnitTypeList.rpt";

        private readonly SubscriptionToken _unitTypeInsertedToken;

        public AllUnitTypesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllUnitTypesViewModel_DisplayName;

            CreateAllUnitTypes();
            _unitTypeInsertedToken = EventAggregator.GetEvent<UnitTypeChangededEvent>().Subscribe(OnUnitTypeChanged);
        }

        public ObservableCollection<SingleUnitTypeViewModel> AllUnitTypes { get; private set; }

        void CreateAllUnitTypes()
        {
            AllUnitTypes = new ObservableCollection<SingleUnitTypeViewModel>(DbConversation
                .Query(new AllUnitTypesQuery())
                .Select(x => new SingleUnitTypeViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllUnitTypes.Where(unitType => unitType.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllUnitTypes.FirstOrDefault(unitType => unitType.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<UnitTypeChangededEvent>().Unsubscribe(_unitTypeInsertedToken);

        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewUnitType())); }
        }

        void NewUnitType()
        {
            EventAggregator.GetEvent<AddUnitTypeEvent>().Publish(null);
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
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditUnitType(), param => ItemsSelected));
            }
        }

        void EditUnitType()
        {
            AllUnitTypes.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditUnitTypeEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemoveUnitType(), param => ItemsSelected));
            }
        }

        void RemoveUnitType()
        {
            var toDelete = AllUnitTypes.Where(vm => vm.IsSelected).ToList();
            var msg = toDelete.Aggregate(Strings.ViewModel_AllUnitTypesViewModel_AskToDelete, (current, item) => current + ("\n" + item.DisplayName));
            if (MessageBox.Show(msg, Strings.ViewModel_AllUnitTypesViewModel_DeleteItems, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            try
            {
                DbConversation.UsingTransaction( () =>
                    {
                        foreach (var instance in toDelete )
                            DbConversation.Delete(instance.UnderlayingObject());
                    });
            }
            catch
            {
                MessageBox.Show(Strings.Error_UnitTypes_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllUnitTypes.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintUnitTypes(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintUnitTypes()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllUnitTypesViewModel_PrintHeader));
        }

        #endregion

        void OnUnitTypeChanged(UnitType unitType)
        {
            var viewmodel = (from vm in AllUnitTypes where vm.Id == unitType.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SingleUnitTypeViewModel(unitType);
                AllUnitTypes.Add(viewmodel);
            }
            else
                viewmodel.ExchangeData(unitType);
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