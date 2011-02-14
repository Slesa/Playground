using System;
using System.ComponentModel;
using System.Windows.Input;
using Godot.IcsEditor.Model;
using Godot.Model;

namespace Godot.IcsEditor.ViewModel
{
    public class ProductionItemViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        private ProductionItem _productionItem;
        private IRepository<ProductionItem> _repository;

        public ProductionItemViewModel(ProductionItem productionItem, IRepository<ProductionItem> repository)
        {
            if (productionItem == null)
                throw new ArgumentNullException("productionItem");

            if (repository == null)
                throw new ArgumentNullException("repository");

            _productionItem = productionItem;
            _repository = repository;
            base.DisplayName = IsNewProductionItem ? "New production item" : _productionItem.Name;
        }

        #region Commands

        ActionCommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new ActionCommand(
                        param => { Save(); CloseCommand.Execute(param); },
                        param => CanSave
                        );
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Public Methods

        public void Remove()
        {
            _repository.Delete(_productionItem.Id);
        }

        public void Save()
        {
//            if (!_productionItem.IsValid)
//                throw new InvalidOperationException("Cannot save invalid prouction item"); //Strings.CustomerViewModel_Exception_CannotSave);

//            if (IsNewProductionItem )
                _repository.Save(_productionItem);

            base.OnPropertyChanged("DisplayName");
        }

        #endregion

        #region Private Helpers

        bool IsNewProductionItem
        {
            get { return !_repository.Contains(_productionItem); }
        }

        bool CanSave
        {
            get { return true; /*String.IsNullOrEmpty(this.ValidateCustomerType()) &&  _productionItem.IsValid; */ }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_productionItem as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_productionItem as IDataErrorInfo).Error; }
        }

        #endregion
    }
}