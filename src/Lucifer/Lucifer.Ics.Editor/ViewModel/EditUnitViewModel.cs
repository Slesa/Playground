using System;
using System.ComponentModel;
using System.Windows.Input;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditUnitViewModel : Screen, IDataErrorInfo
    {
        readonly UnitModel _unitModel;

        public EditUnitViewModel()
        {
            _unitModel = new UnitModel(new Unit());
            Title = Strings.EditUnitView_TitleNew;
            DisplayName = Strings.EditUnitView_NewUnit;

        }

        public string Title { get; private set; }

        public string Name
        {
            get { return _unitModel.Name; }
            set
            {
                if (value == _unitModel.Name) return;
                _unitModel.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public string Contraction
        {
            get { return _unitModel.Contraction; }
            set
            {
                if (value == _unitModel.Contraction) return;
                _unitModel.Contraction = value;
                NotifyOfPropertyChange(() => Contraction);
            }
        }

        public UnitType UnitType
        {
            get { return _unitModel.UnitType; }
            set
            {
                if (value == _unitModel.UnitType)
                    return;
                _unitModel.UnitType = value;
                NotifyOfPropertyChange(() => ParentUnit);
                NotifyOfPropertyChange(() => UnitType);
            }
        }

        public Unit ParentUnit
        {
            get { return _unitModel.Parent; }
            set
            {
                if (value == _unitModel.Parent)
                    return;

                //if (_unitModel.Parent != null)
                //    _unitModel.Parent.RemoveChild(_unitModel);

                _unitModel.Parent = value.Id == 0 ? null : value;

                //if (_unitModel.Parent != null)
                //    _unitModel.Parent.AddChild(_unitModel.Unit);

                NotifyOfPropertyChange(() => ParentUnit);
                NotifyOfPropertyChange(() => UnitType);
            }
        }

        public string FactorToParent
        {
            get { return _unitModel.FactorToParent; }
            set
            {
                if (value == _unitModel.FactorToParent)
                    return;
                _unitModel.FactorToParent = value;
                NotifyOfPropertyChange(() => FactorToParent);
            }
        }

        public bool Purchasing
        {
            get { return _unitModel.Purchasing; }
            set
            {
                if (value == _unitModel.Purchasing)
                    return;
                _unitModel.Purchasing = value;
                NotifyOfPropertyChange(() => Purchasing);
            }
        }

        public bool Reciping
        {
            get { return _unitModel.Reciping; }
            set
            {
                if (value == _unitModel.Reciping)
                    return;
                _unitModel.Reciping = value;
                NotifyOfPropertyChange(() => Reciping);
            }
        }
        
        #region Validation

        public string this[string propertyName]
        {
            get
            {
                var error = (_unitModel as IDataErrorInfo)[propertyName];
                //CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_unitModel as IDataErrorInfo).Error; }
        }

        #endregion

        #region Button commands

        public void Close()
        {
            TryClose();
        }

        #endregion

        #region Module information

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Unit.png"; }
        }

        #endregion
    }
}