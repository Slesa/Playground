using System;
using Caliburn.Micro;
using DataGridDragDrop.Data;

namespace DataGridDragDrop.ViewModels
{
    public class GridItemRowViewModel : PropertyChangedBase
    {
        readonly GridItem _gridItem;

        public GridItemRowViewModel(GridItem gridItem)
        {
            _gridItem = gridItem;
        }

        public int Id
        {
            get { return _gridItem.Id; } 
            set
            {
                if (_gridItem.Id == value) return;
                _gridItem.Id = value;
                NotifyOfPropertyChange(()=>Id);
            }
        }

        public string Name
        {
            get { return _gridItem.Name; }
        }

        public bool Visible
        {
            get { return _gridItem.Visible; }
            set
            {
                if( _gridItem.Visible==value ) return;
                _gridItem.Visible = value;
                NotifyOfPropertyChange(()=>Visible);
            }
        }

        public bool Checked
        {
            get { return _gridItem.Checked; }
            set
            {
                if (_gridItem.Checked == value) return;
                _gridItem.Checked = value;
                NotifyOfPropertyChange(()=>Checked);
            }
        }

        public int IntValue
        {
            get { return _gridItem.IntValue; }
            set 
            { 
                if (_gridItem.IntValue == value) return;
                _gridItem.IntValue = value;
                NotifyOfPropertyChange(()=>IntValue);
            }
        }

        public string TextValue
        {
            get { return _gridItem.TextValue; }
            set
            {
                if (_gridItem.TextValue == value) return;
                _gridItem.TextValue = value;
                NotifyOfPropertyChange(() => TextValue);
            }
        }

        public string Selection
        {
            get { return _gridItem.Selection.ToString(); }
            set
            {
                Selectable result;
                if (!Enum.TryParse(value, out result)) return;
                _gridItem.Selection = result;
                NotifyOfPropertyChange(()=>Selection);
            }
        }
    }
}