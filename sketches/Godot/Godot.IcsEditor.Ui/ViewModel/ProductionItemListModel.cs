using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using Godot.IcsModel.Entities;
using Godot.Model;

namespace Godot.IcsEditor.ViewModel
{
    public class OpenListEventArgs : EventArgs{}

    public class ProductionItemListModel : ViewModelBase
    {
        readonly IRepository<ProductionItem> _repository;
        ProductionItem _currentItem;

        public ProductionItemListModel(IRepository<ProductionItem> repository)
        {
            _repository = repository;
        }

        public ObservableCollection<ProductionItem> ProductionItems
        //public List<ProductionItem> ProductionItems
        {
            get
            {
                //var result = new List<ProductionItem>(_repository.FindAll());
                var result = new ObservableCollection<ProductionItem>(_repository.FindAll());
                if (result.Count == 0)
                {
                    _repository.Save(new ProductionItem { Name = "Bier vom Fass" });
                    _repository.Save(new ProductionItem { Name = "Weizenbier vom Fass" });
                    result = new ObservableCollection<ProductionItem>(_repository.FindAll());
                    //result = new List<ProductionItem>(_repository.FindAll());
                }
                return result;
            }
        }

        ActionCommand _newCommand;
        public ICommand New
        {
            get
            {
                if (_newCommand == null)
                    _newCommand = new ActionCommand(param =>OnNewProductionItem() );

                return _newCommand;
            }
        }

        ActionCommand _editCommand;
        public ICommand Edit
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new ActionCommand(param => OnEditProductionItem());

                return _editCommand;
            }
        }

        void OnEditProductionItem()
        {
            var dlg = new DlgProductionItem();
            dlg.DataContext = this;
            dlg.ShowDialog();
        }

        static void OnNewProductionItem()
        {
            var dlg = new DlgProductionItem();
            dlg.DataContext = new ProductionItem();
            dlg.ShowDialog();
        }

    }
}
