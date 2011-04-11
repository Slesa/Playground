using System.ComponentModel;
using System.Windows.Input;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditUnitTypeViewModel : Screen, IDataErrorInfo
    {
        readonly UnitTypeModel _unitTypeModel;

        public EditUnitTypeViewModel()
        {
            _unitTypeModel = new UnitTypeModel(new UnitType());
        }

        public string Name 
        { 
            get { return _unitTypeModel.Name; }
            set
            {
                if (value == _unitTypeModel.Name) return;
                _unitTypeModel.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string this[string propertyName]
        {
            get
            {
                var error = (_unitTypeModel as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_unitTypeModel as IDataErrorInfo).Error; }
        }
    }
}