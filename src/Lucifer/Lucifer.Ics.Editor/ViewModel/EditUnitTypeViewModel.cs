using System.ComponentModel;
using System.Windows.Input;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditUnitTypeViewModel : Screen, IDataErrorInfo
    {
        readonly UnitTypeModel _unitTypeModel;

        public EditUnitTypeViewModel()
        {
            _unitTypeModel = new UnitTypeModel(new UnitType());
            Title = Strings.EditUnitTypeView_TitleNew;
            DisplayName = Strings.EditUnitTypeView_NewUnitType;

        }

        public string Title { get; private set; }

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

    #region Validation

        public string this[string propertyName]
        {
            get
            {
                var error = (_unitTypeModel as IDataErrorInfo)[propertyName];
                //CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_unitTypeModel as IDataErrorInfo).Error; }
        }

    #endregion

        bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
                NotifyOfPropertyChange(() => CanEdit);
                NotifyOfPropertyChange(() => CanCancel);
            }
        }

        protected override void OnDeactivate(bool isClosing)
        {
            if (isClosing && CanCancel)
                Cancel();
        }

        public bool CanEdit { get { return IsReadOnly; } }
        public bool CanCancel { get { return !IsReadOnly; } }
        public bool CanSave { get { return Error == null; } }

        public void Cancel()
        {
            IsReadOnly = true;
        }

        #region Button commands

        public void Save()
        {

        }

        public void Close()
        {
            TryClose();
        }

        #endregion

        #region Module information

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/UnitType.png"; }
        }

        #endregion
    }
}